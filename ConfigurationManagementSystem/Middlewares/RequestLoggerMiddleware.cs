using ConfigurationManagementSystem.Services;
using System.Diagnostics;

namespace ConfigurationManagementSystem.Middlewares
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next)
        { 
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, LoggerService logger)
        {
            var stopwatch = Stopwatch.StartNew();
            var httpRequest = context.Request;
            httpRequest.EnableBuffering();

            var bodyJson = await (new StreamReader(httpRequest.Body).ReadToEndAsync());
            logger.Info($"Start request - Method: {httpRequest.Method} Path: {httpRequest.Path} Query: {httpRequest.QueryString} Body: {bodyJson}");
            context.Request.Body.Position = 0;

            await _next.Invoke(context);

            var httpResponse = context.Response;
            stopwatch.Stop();
            logger.Info($"End request - Status: {httpResponse.StatusCode} Response Size: {httpResponse.ContentLength} Time: {stopwatch.Elapsed}");
        }
    }
}
