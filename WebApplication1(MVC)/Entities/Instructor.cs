using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1_MVC_.Entitys
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }
        [Required, MaxLength(30) ]
        public string InstructorName { get; set; }
        [Required, MaxLength(40),EmailAddress ]
        public string InstructorEmail { get; set; }
        [Required,MaxLength(11) ]
        public string InstructorPhone { get; set; }
        [Required, MaxLength(350)]
        public string InstructorBio { get; set; }
        [Required, MinLength(3)]
        public string InstructorPassword { get; set; }
        public ICollection<Course> courses { get; set; } = new List<Course>();
    }
}
