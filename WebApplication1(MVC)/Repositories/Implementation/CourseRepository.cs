using Microsoft.EntityFrameworkCore;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Repositories.Interface;

namespace WebApplication1_MVC_.Repositories.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        private readonly APPDbContext _context;

        public CourseRepository(APPDbContext context)
        {
            _context = context;
        }
        public async Task<List<Course>> GetAllAsync()
        {
            var AllCourses = await _context.courses
                .Include(c => c.Instructor)
                .ToListAsync();
            return AllCourses;
        }
        public async Task<Course?> GetByIdAsync(int id)
        {
            var CourseId = await _context.courses
                .Include(c => c.Instructor)
                .FirstOrDefaultAsync(C => C.CourseId == id);
            if (CourseId == null)
            {
                return null;
            }
            else
            {
                return CourseId;
            }
        }
        public async Task<Course> AddAsync(Course course)
        {
            var AddCourse = await _context.courses.AddAsync(course);
            await _context.SaveChangesAsync();
            return AddCourse.Entity;
        }
        public async Task<Course?> UpdateAsync(int id, Course course)
        {
            var UpdatedCourse = await _context.courses.FirstOrDefaultAsync(C =>C.CourseId == id);
            if(UpdatedCourse == null)
            {
                return null;
            }
            UpdatedCourse.CourseTitle = course.CourseTitle;
            UpdatedCourse.Price = course.Price;
            UpdatedCourse.Description = course.Description;
            UpdatedCourse.CreatedDate = course.CreatedDate;
            UpdatedCourse.InstructorId = course.InstructorId;
            await _context.SaveChangesAsync();
            return UpdatedCourse;
        }
        public async Task<bool?> DeleteAsync(int id)
        {
            var CourseId = await _context.courses.FirstOrDefaultAsync(C => C.CourseId == id);
            if (CourseId == null)
            {
                return null;
            }
            else
            {
                _context.courses.Remove(CourseId);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}
