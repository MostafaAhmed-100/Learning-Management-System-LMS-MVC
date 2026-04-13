using NuGet.Protocol.Core.Types;

namespace WebApplication1_MVC_.DTOs
{
    public class EnrollmentResponseDTO
    {
        public string CourseTitle { get; set; }
        public float CoursePrice { get; set; }

        public int CourseId { get; set; }

        public string StudentName { get; set; }

        public int StudentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}