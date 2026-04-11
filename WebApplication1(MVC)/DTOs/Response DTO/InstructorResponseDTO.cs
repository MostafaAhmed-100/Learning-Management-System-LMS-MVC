using System.ComponentModel.DataAnnotations;

namespace WebApplication1_MVC_.DTOs
{
    public class InstructorResponseDTO
    {
        public int InstracrorId{ get; set; }
        public string InstructorName { get; set; }
        public string InstructorEmail { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorBio { get; set; }
        public List<string> Coures { get; set; }
    }
}
