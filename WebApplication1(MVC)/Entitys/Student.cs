using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1_MVC_.Entitys
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required, MaxLength(35)]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required, MaxLength(355)]
        public string Student_Email { get; set; }
        public ICollection<Enrollment> enrollments { get; set; }
    }
}