using AdminDashboard.Services;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObject.IdentityModules;

namespace AdminDashboard.Controllers
{
    public class AdminController(UserManager<ApplicationUser> _userManager,
                                 SignInManager<ApplicationUser> _signInManager,
                                 IAuthApiService _authApiService) : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var userDto = await _authApiService.RegisterApiAsync(registerDto);

                if (userDto != null && !string.IsNullOrEmpty(userDto.Token))
                {
                    Response.Cookies.Append("Token", userDto.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.Now.AddHours(1)
                    });
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(registerDto);
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto logInDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(logInDto.Email);
                if (user is not null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, logInDto.Password, false, false); ;
                    if (result.Succeeded)
                    {
                        var userDto = await _authApiService.GetTokenFromApiAsync(logInDto);

                        if (userDto != null && !string.IsNullOrEmpty(userDto.Token))
                        {
                            Response.Cookies.Append("Token", userDto.Token, new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = false,
                                Expires = DateTime.Now.AddHours(1)
                            });
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(logInDto);
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn), "Admin");
        }

    }
}
