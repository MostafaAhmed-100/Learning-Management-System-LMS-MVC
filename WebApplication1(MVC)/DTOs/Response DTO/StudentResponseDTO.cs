using System.ComponentModel.DataAnnotations;
using WebApplication1_MVC_.Entitys;

namespace WebApplication1_MVC_.DTOs
{
    public class StudentResponseDTO
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public int StudentAge { get; set; }
        [EmailAddress]
        public string Student_Email { get; set; }
        public List<string> Enrollments { get; set; }
    }
}
