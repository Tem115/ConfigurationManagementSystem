using ConfigurationManagementSystem.Attributes;
using ConfigurationManagementSystem.Models.InputModels;
using ConfigurationManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ConfigurationManagementSystem.Controllers
{
    [Authorize]
    public class ConfigurationController(LoggerService logger, ConfigurationService configurationService) : CustomControllerBase(logger)
    {
        [HttpPost]
        [SwaggerOperation("Создание конфигурации")]
        [DefaultException("Ошибка создания конфигурации")]
        public async Task<IActionResult> CreateConfigurationAsync([FromBody] ConfigurationInputModel configurationInputModel)
        {
            return Ok(await configurationService.CreateConfigurationAsync(configurationInputModel));
        }

        [HttpPatch]
        [SwaggerOperation("Изменение конфигурации")]
        [DefaultException("Ошибка изменения конфигурации")]
        public async Task<IActionResult> UpdateConfigurationAsync([FromBody] ConfigurationInputModel configurationInputModel)
        {
            return Ok(await configurationService.UpdateConfigurationAsync(configurationInputModel, HttpContext));
        }

        [HttpGet]
        [SwaggerOperation("Получение списка конфигураций")]
        [DefaultException("Ошибка получения списка конфигураций")]
        public async Task<IActionResult> GetConfigurationListAsync([FromQuery] ConfigurationSearchCriteria? configurationSearchCriteria)
        {
            return Ok(await configurationService.GetConfigurationListAsync(configurationSearchCriteria));
        }
    }
}
