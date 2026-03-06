namespace WebApplication1_MVC_.DTOs
{
    public class CourseDTOs
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string InstractorName { get; set; }
    }
}