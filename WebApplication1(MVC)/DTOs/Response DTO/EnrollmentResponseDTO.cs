namespace WebApplication1_MVC_.DTOs
{
    public class EnrollmentDTO
    {
        public string CourseTitle { get; set; }
        public float CoursePrice { get; set; }

        public string StudentName { get; set; }
        public string InstructorName { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}