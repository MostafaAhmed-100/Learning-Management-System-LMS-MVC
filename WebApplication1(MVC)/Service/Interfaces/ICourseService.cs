using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Entitys;

namespace WebApplication1_MVC_.Service.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseResponseDTO>> GetAllCoursesAsync();
        Task<CourseResponseDTO?> GetCourseByIdAsync(int id);
        Task<CourseResponseDTO> AddCourseAsync(CourseRequestDto course_request_dto);
        Task<CourseResponseDTO?> UpdateCourseAsync(int id, CourseRequestDto course_request_dto);
        Task<bool> DeleteCourseAsync(int id);
    }
}
