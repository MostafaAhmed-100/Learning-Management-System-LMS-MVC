using WebApplication1_MVC_.Entitys;

namespace WebApplication1_MVC_.Repositories.Interface
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task <Course> AddAsync(Course course);
        Task<Course?> UpdateAsync(int id ,Course course);
        Task<bool?> DeleteAsync(int id);
    }
}
