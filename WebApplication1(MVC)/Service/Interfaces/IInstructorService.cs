using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;

namespace WebApplication1_MVC_.Service.Interfaces
{
    public interface IInstructorService
    {
        Task<List<InstructorResponseDTO>> GetAllInstructorsAsync();
        Task<InstructorResponseDTO?> GetInstructorByIdAsync(int id);
        Task<InstructorResponseDTO> AddInstructorAsync(InstructorRequestDTO instructor_requestDTO);
        Task<InstructorResponseDTO?> UpdateInstructorAsync(int id, InstructorRequestDTO instructor_requestDTO);
        Task<bool> DeleteInstructorAsync(int id);
    }
}
