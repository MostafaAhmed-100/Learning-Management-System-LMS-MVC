using Microsoft.VisualBasic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1_MVC_.DTOs.Request_DTOs
{
    public class StudentRequestDto
    {
        [Required,MinLength(3),MaxLength(50)]
        public string StudentName { get; set; }
        [EmailAddress]
        public string StudentEmail { get; set; }
        [Required]
        public int StudentAge { get; set; }
        [Required,MinLength(8)]
        public string StudentPassword { get; set; }
    }
}
