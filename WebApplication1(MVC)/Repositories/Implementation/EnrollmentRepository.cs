using Microsoft.EntityFrameworkCore;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Repositories.Interface;

namespace WebApplication1_MVC_.Repositories.Implementation
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly APPDbContext _context;
        
        public EnrollmentRepository (APPDbContext context)
        {
            _context = context;
        }
        public async Task<List<Enrollment>> GetAllAsync()
        {
            var GetAll = await _context.enrollments
                .Include(I => I.Student)
                .Include(C => C.Course)
                .ToListAsync();
            return GetAll;
        }

        public async Task<Enrollment?> GetByStudentAndCourseIdAsync(int StudentId, int CourseId)
        {
            var GetById = await _context.enrollments
                .FirstOrDefaultAsync(E => E.StudentId == StudentId
                && E.CourseId == CourseId);
            if (GetById == null)
            {
                return null;
            }
            return GetById;

        }
        public async Task<Enrollment> AddAsync(Enrollment enrollment)
        {
            var AddEnrollment = await _context.enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
            return AddEnrollment.Entity;
        }
        public async Task<bool?> DeleteByIdAsync(int StudentId, int CourseId)
        {
            var GetById = await _context.enrollments
                .FirstOrDefaultAsync(E => E.StudentId == StudentId
                && E.CourseId == CourseId);
            if (GetById == null)
            {
                return null;
            }
            _context.enrollments.Remove(GetById);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
