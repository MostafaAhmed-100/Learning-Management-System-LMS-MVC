using System.ComponentModel.DataAnnotations;

namespace WebApplication1_MVC_.DTOs.Request_DTOs
{
    public class RegisterAdminDto
    {
        [Required]
        public string UserName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(4)]
        public string Password { get; set; }
    }
}
