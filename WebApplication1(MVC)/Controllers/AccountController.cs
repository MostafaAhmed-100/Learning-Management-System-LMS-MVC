using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Models;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid) return View(loginDto);

            var role = await _accountService.LoginAsync(loginDto);

            if (role != null)
            {
                return RedirectToAction("All_Students", "Student");
            }

            ModelState.AddModelError("", "بيانات الدخول غير صحيحة");
            return View(loginDto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RegisterAdmin() => View();

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterAdminDto registerDto)
        {
            if (!ModelState.IsValid) return View(registerDto);

            var result = await _accountService.RegisterAdminAsync(registerDto);
            if (result.Succeeded) return RedirectToAction("Login");

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View(registerDto);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Login");
        }
        
    }
}
