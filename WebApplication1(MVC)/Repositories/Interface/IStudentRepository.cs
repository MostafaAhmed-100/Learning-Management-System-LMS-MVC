using WebApplication1_MVC_.Entitys;

namespace WebApplication1_MVC_.Repositories.Interface
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        
        Task<Student> AddAsync(Student student);
        Task<Student?> UpdateAsync(int id, Student student);

        Task<bool?> DeleteByIdAsync(int id);

    }
}
