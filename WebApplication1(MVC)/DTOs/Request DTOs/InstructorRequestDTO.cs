using System.ComponentModel.DataAnnotations;

namespace WebApplication1_MVC_.DTOs.Request_DTOs
{
    public class InstructorRequestDTO
    {
        [Required, MaxLength(40)]
        public string InstructorName { get; set; }
        [Required, MaxLength(40), EmailAddress]
        public string InstructorEmail { get; set; }
        [Required, MaxLength(11)]
        public string InstructorPhone { get; set; }
        [Required, MaxLength(350)]
        public string InstructorBio { get; set; }

        [MinLength(8)]
        public string InstructorPassword { get; set; }
        [Required ]
        public int CourseCount { get; set; }
        [Required]
        public string CourseName { get; set; }

    }
}
