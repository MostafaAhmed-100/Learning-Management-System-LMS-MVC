using WebApplication1_MVC_.Entitys;

namespace WebApplication1_MVC_.Repositories.Interface
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllAsync();

        Task<Enrollment> AddAsync(Enrollment enrollment);
        Task<Enrollment?> GetByStudentAndCourseIdAsync(int StudentId,int CourseId);

        Task<bool?> DeleteByIdAsync(int StudentId, int CourseId);
    }
}
