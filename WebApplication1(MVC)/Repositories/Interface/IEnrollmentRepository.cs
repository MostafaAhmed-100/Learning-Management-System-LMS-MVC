using WebApplication1_MVC_.Entitys;

namespace WebApplication1_MVC_.Repositories.Interface
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllAsync();

        Task<Enrollment?> AddAsync(Enrollment enrollment);

        Task<bool?> DeleteByIdAsync(int StudentId, int CourseId);

    }
}
