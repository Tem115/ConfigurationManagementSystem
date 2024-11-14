using ConfigurationManagementSystem.Attributes;
using ConfigurationManagementSystem.Services;
using System.Net.Mime;

namespace ConfigurationManagementSystem.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        { 
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, LoggerService logger)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex) 
            {
                logger.Error(ex.ToString());

                var defaultExceptionAttribute = context.GetEndpoint()?.Metadata.GetMetadata<DefaultExceptionAttribute>();
                var defaultExceptionMessage = defaultExceptionAttribute?.Message ?? "Не удалось выполнить данный запрос";

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Text.Plain;
                await context.Response.WriteAsync(defaultExceptionMessage);
            }
        }
    }
}
