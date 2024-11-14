using ConfigurationManagementSystem.Attributes;
using ConfigurationManagementSystem.Models.InputModels;
using ConfigurationManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ConfigurationManagementSystem.Controllers
{
    public class AuthenticationController(LoggerService logger, AuthenticationService authenticationService) : CustomControllerBase(logger)
    {
        [HttpPost]
        [SwaggerOperation("Cookies аутентификация")]
        [DefaultException("Ошибка аутентификации")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest authenticateRequest)
        {
            return await authenticationService.Login(authenticateRequest, HttpContext);
        }
    }
}
