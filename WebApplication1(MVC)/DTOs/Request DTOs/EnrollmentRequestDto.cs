using System.ComponentModel.DataAnnotations;

namespace WebApplication1_MVC_.DTOs.Request_DTOs
{
    public class EnrollmentRequestDto
    {
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}