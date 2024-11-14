using AutoMapper;
using ConfigurationManagementSystem.Models.InputModels;
using ConfigurationManagementSystem.Models.OutputModels;
using Databases.DbContexts;
using Databases.Entities;
using Microsoft.EntityFrameworkCore;
using Version = Databases.Entities.Version;
using Microsoft.AspNetCore.SignalR;
using ConfigurationManagementSystem.Hubs;
using System.Security.Claims;

namespace ConfigurationManagementSystem.Services
{
    public class ConfigurationService(IMapper mapper, ConfigurationsDbContext configurationDbContext, IHubContext<NotificationsHub> notificationsHub)
    {
        public async Task<ConfigurationOutputModel> CreateConfigurationAsync(ConfigurationInputModel configurationInputModel)
        {
            Configuration configuration = new Configuration()
            {
                Name = configurationInputModel.Name
            };
            await configurationDbContext.Configurations.AddAsync(configuration);
            await configurationDbContext.SaveChangesAsync();
            ConfigurationOutputModel configurationOutputModel = mapper.Map<ConfigurationOutputModel>(configuration);
            configurationOutputModel.HotKeys = await CreateHotKeysAsync(configurationInputModel.HotKeys, configuration.Id);
            return configurationOutputModel;
        }

        public async Task<ConfigurationOutputModel> UpdateConfigurationAsync(ConfigurationInputModel configurationInputModel, HttpContext httpContext)
        {
            Configuration configuration = configurationDbContext.Configurations.First(c => c.Id.Equals(configurationInputModel.Id!.Value));
            configuration.Name = configurationInputModel.Name;
            configuration.HotKeys = mapper.Map<List<HotKey>>(configurationInputModel.HotKeys);
            configurationDbContext.Configurations.Update(configuration);
            var updated = await configurationDbContext.SaveChangesAsync();
            if (updated > 0 && configurationDbContext.Versions.OrderByDescending(v => v.UpdateDate).First().ConfigurationId.Equals(configuration.Id))
            {
                await notificationsHub.Clients.All.SendAsync("Notifications", $"Пользователь {httpContext.User.Identity!.Name} изменил текущую конфигурацию");
            }
            return mapper.Map<ConfigurationOutputModel>(configuration);
        }

        public async Task<List<ConfigurationOutputModel>> GetConfigurationListAsync(ConfigurationSearchCriteria? configurationSearchCriteria)
        {
            IQueryable<Configuration> configurations = configurationDbContext.Configurations.Include(c => c.HotKeys).ThenInclude(hk => hk.Command).AsNoTracking();
            if (configurationSearchCriteria?.Name != null)
                configurations = configurations.Where(c => c.Name.Contains(configurationSearchCriteria.Name));
            if (configurationSearchCriteria?.CreateDate != null)
                configurations = configurations.Where(c => c.CreateDate.Date == configurationSearchCriteria.CreateDate.Value.Date);
            return mapper.Map<List<ConfigurationOutputModel>>(await configurations.ToListAsync());
        }

        public async Task<List<HotKeyOutputModel>> CreateHotKeysAsync(List<HotKeyInputModel> hotKeysInputModel, Guid ConfigurationId)
        {
            var hotKeys = await Task.WhenAll(hotKeysInputModel.ConvertAll(async inputModel => new HotKey()
            {
                ConfigurationId = ConfigurationId,
                Hex = inputModel.Hex,
                CommandId = inputModel.Command.Id.HasValue ? inputModel.Command.Id.Value : (await CreateCommandAsync(inputModel.Command!)).Id
            }));
            await configurationDbContext.HotKeys.AddRangeAsync(hotKeys);
            await configurationDbContext.SaveChangesAsync();
            return mapper.Map<List<HotKeyOutputModel>>(configurationDbContext.HotKeys.Include(hk => hk.Command).Where(hk => hotKeys.Select(h => h.Id).Contains(hk.Id)).ToList());
        }

        public async Task<CommandOutputModel> CreateCommandAsync(CommandInputModel commandInputModel)
        {
            Command command = mapper.Map<Command>(commandInputModel);
            await configurationDbContext.Commands.AddAsync(command);
            await configurationDbContext.SaveChangesAsync();
            return mapper.Map<CommandOutputModel>(command);
        }

        public async Task<List<VersionOutputModel>> GetVersionListAsync()
        {
            return mapper.Map<List<VersionOutputModel>>(await configurationDbContext.Versions.AsNoTracking().ToListAsync());
        }

        public async Task<VersionOutputModel> SelectVersionsAsync(Guid configurationId, HttpContext httpContext)
        {
            Version version = new Version()
            {
                ConfigurationId = configurationId
            };
            await configurationDbContext.Versions.AddAsync(version);
            await configurationDbContext.SaveChangesAsync();
            await notificationsHub.Clients.All.SendAsync("Notifications", $"Пользователь {httpContext.User.Identity!.Name} установил конфигурацию");
            return mapper.Map<VersionOutputModel>(version);
        }
    }
}
