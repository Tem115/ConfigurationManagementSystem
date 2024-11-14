using ConfigurationManagementSystem.Attributes;
using ConfigurationManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ConfigurationManagementSystem.Controllers
{
    [Authorize]
    public class VersionController(LoggerService logger, ConfigurationService configurationService) : CustomControllerBase(logger)
    {
        [HttpGet]
        [SwaggerOperation("Получение списка версий конфигураций")]
        [DefaultException("Ошибка получения списка версий")]
        public async Task<IActionResult> GetVersionListAsync()
        {
            return Ok(await configurationService.GetVersionListAsync());
        }

        [HttpPost]
        [SwaggerOperation("Сменить текущую конфигурацию")]
        [DefaultException("Ошибка смены текущей конфигурации")]
        public async Task<IActionResult> SelectVersionsAsync(Guid configurationId)
        {
            return Ok(await configurationService.SelectVersionsAsync(configurationId, HttpContext));
        }
    }
}
