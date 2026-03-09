using WebApplication1_MVC_.Entitys;

namespace WebApplication1_MVC_.Repositories.Interface
{
    public interface IInstructorRepository
    {
        Task<List<Instructor>> GetAllAsync();

        Task<Instructor> AddAsync(Instructor instructor);
        Task<Instructor?> GetByIdAsync(int Id);
        Task<Instructor?> UpdateById(int Id , Instructor instructor);

        Task<bool?> DeleteByIdAsync(int Id);
    }
}
