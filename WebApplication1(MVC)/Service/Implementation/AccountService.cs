using Microsoft.AspNetCore.Identity;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Models;
using WebApplication1_MVC_.Service.Interfaces;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<string?> LoginAsync(LoginRequestDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return null;

        var result = await _signInManager.PasswordSignInAsync(user.UserName!, loginDto.Password, loginDto.RememberMe, false);

        if (result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault(); 
        }
        return null;
    }

    public async Task<IdentityResult> RegisterAdminAsync(RegisterAdminDto registerDto)
    {
        var user = new ApplicationUser { UserName = registerDto.UserName, Email = registerDto.Email };
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Admin");
        }
        return result;
    }

    public async Task LogoutAsync() => await _signInManager.SignOutAsync();
}