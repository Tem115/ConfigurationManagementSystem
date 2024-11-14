using ConfigurationManagementSystem.Middlewares;

namespace ConfigurationManagementSystem.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestLoggerMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}
