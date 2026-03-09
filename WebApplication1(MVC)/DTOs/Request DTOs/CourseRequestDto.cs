using System.ComponentModel.DataAnnotations;

namespace WebApplication1_MVC_.DTOs.Request_DTOs
{
    public class CourseRequestDto
    {
        [Required, MaxLength(50), MinLength(3)]
        public string CourseTitle { get; set; }
        [Required, MaxLength(500), MinLength(10)]
        public string CourseDescription { get; set; }
        [Required, Range(minimum: 100, maximum: 10000)]
        public float CoursePrice { get; set; }
        [Required]
        public int InstructorId { get; set; }
    }
}
