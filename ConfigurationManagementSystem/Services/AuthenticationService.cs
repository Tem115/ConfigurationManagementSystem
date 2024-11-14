using ConfigurationManagementSystem.Models.InputModels;
using Databases.DbContexts;
using Databases.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConfigurationManagementSystem.Services
{
    public class AuthenticationService(ConfigurationsDbContext configurationDbContext)
    {
        public async Task<IActionResult> Login(AuthenticateRequest authenticateRequest, HttpContext httpContext)
        {
            User user = await configurationDbContext.Users.FirstAsync(u => u.Login.Equals(authenticateRequest.Login));
            if (user is not null && user.Password == authenticateRequest.Password)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, authenticateRequest.Login)
                };
                // создаем объект ClaimsIdentity
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                // установка аутентификационных куки
                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return new OkResult();
            }
            else
                return new UnauthorizedResult();
        }
    }
}
