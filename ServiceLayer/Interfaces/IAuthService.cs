using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Models;

namespace ServiceLayer.Interfaces;

public interface IAuthService
{
    Task<string> GenerateTokenString(IdentityUser user);
    Task<IActionResult> Login(string email, string password);
    Task<IActionResult> Registration(UserModel user);
}
