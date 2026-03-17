using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;

namespace WebApplication1_MVC_.Service.Interfaces
{
    public interface IEnrollmentService
    {
        Task<List<EnrollmentResponseDTO>> GetAllEnrollmentsAsync();
        Task<bool> EnrollStudentAsync(EnrollmentRequestDto enrollmentDto);
        Task<bool> UnenrollStudentAsync(int studentId, int courseId);
    }
}
