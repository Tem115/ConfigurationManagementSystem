using ConfigurationManagementSystem.Services;

namespace ConfigurationManagementSystem.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationService>();
            services.AddScoped<ConfigurationService>();
            services.AddScoped<LoggerService>();

            return services;
        }
    }
}
