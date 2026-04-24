namespace WebApplication1_MVC_.DTOs
{
    public class CourseResponseDTO
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public string CourseDescription { get; set; }
        public float CoursePrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public string InstractorName { get; set; }

        public int InstructorId { get; set; }
    }
}