using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1_MVC_.Entitys
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required, MaxLength(30)]
        public string Title { get; set; }
        [Required, MaxLength(300)]
        public string Description { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public Instructor Instructor { get; set; }
        [Required, ForeignKey(nameof(InstructorId))]
        public int InstructorId { get; set; }
        public ICollection<Enrollment> enrollments{ get; set; }
    }
}
