using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1_MVC_.Entitys
{
    public class Instructor
    {
        [Key]
        public int InstructorId { get; set; }
        [Required, MaxLength(30) ]
        public string Name { get; set; }
        [Required, MaxLength(40) ]
        public string Email { get; set; }
        [Required,MaxLength(11) ]
        public string Phone { get; set; }
        [Required, MaxLength(350)]
        public string Bio { get; set; }
        public ICollection<Course> courses { get; set; } = new List<Course>();
    }
}
