using ConfigurationManagementSystem.Extensions;
using ConfigurationManagementSystem.Hubs;
using Databases.DbContexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using System.Text.Json.Serialization;

namespace ConfigurationManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddNLog();

            var services = builder.Services;

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
            });
            services.AddAutoMapper(typeof(Program));
            services.AddSignalR();
            services.AddServices();
            services.AddDbContext<ConfigurationsDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("ConfigurationsDb")));
            services.AddRouting(routeOptions => routeOptions.LowercaseUrls = true);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            services.AddAuthorization();

            var app = builder.Build();

            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseMiddlewares();

            app.MapHub<NotificationsHub>("/notifications");

            app.Run();
        }
    }
}
