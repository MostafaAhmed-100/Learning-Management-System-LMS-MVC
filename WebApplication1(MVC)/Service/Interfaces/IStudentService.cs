using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;

namespace WebApplication1_MVC_.Service.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentResponseDTO>> GetAllStudentsAsync();
        Task<StudentResponseDTO?> GetStudentByIdAsync(int id);
        Task<StudentResponseDTO> AddStudentAsync(StudentRequestDto student_requestDto);
        Task<StudentResponseDTO?> UpdateStudentAsync(int id, StudentRequestDto student_requestDto);
        Task<bool> DeleteStudentAsync(int id);
    }
}
