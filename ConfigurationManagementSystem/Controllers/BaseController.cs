using ConfigurationManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomControllerBase : ControllerBase
    {
        private protected readonly LoggerService _logger;

        public CustomControllerBase(LoggerService logger)
        {
            _logger = logger;
        }

    }
}