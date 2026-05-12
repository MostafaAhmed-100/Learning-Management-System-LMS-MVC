using System.ComponentModel.DataAnnotations;

namespace WebApplication1_MVC_.DTOs.Request_DTOs
{
    public class LoginRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
