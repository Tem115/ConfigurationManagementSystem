namespace ConfigurationManagementSystem.Services
{
    public class LoggerService
    {
        private const string LOG_TEMPLATE = "[{traceId}] {text}";

        private readonly ILogger<LoggerService> _logger;
        private readonly Guid _traceId;

        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
            _traceId = Guid.NewGuid();
        }

        private void Log(Action<string?, object?[]> func, string message) => func(LOG_TEMPLATE, [_traceId.ToString(), message]);

        public void Debug(string message) => Log(_logger.LogDebug, message);

        public void Info(string message) => Log(_logger.LogInformation, message);

        public void Warning(string message) => Log(_logger.LogWarning, message);

        public void Error(string message) => Log(_logger.LogError, message);
    }
}
