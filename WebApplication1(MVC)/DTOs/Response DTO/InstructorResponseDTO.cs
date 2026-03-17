using System.ComponentModel.DataAnnotations;

namespace WebApplication1_MVC_.DTOs
{
    public class InstructorResponseDTO
    {
        public string InstructorName { get; set; }
        public string InstructorEmail { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorBio { get; set; }
        public int CourseCount { get; set; }
    }
}
