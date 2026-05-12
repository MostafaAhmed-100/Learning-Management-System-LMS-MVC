using Microsoft.AspNetCore.Identity;
using WebApplication1_MVC_.DTOs.Request_DTOs;

namespace WebApplication1_MVC_.Service.Interfaces
{
    public interface IAccountService
    {
        Task<string?> LoginAsync(LoginRequestDto loginDto);
        Task<IdentityResult> RegisterAdminAsync(RegisterAdminDto registerDto);
        Task LogoutAsync();
    }
}
