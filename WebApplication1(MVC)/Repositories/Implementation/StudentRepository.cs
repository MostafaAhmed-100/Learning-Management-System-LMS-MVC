using Microsoft.EntityFrameworkCore;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Repositories.Interface;

namespace WebApplication1_MVC_.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly APPDbContext _context;

        public StudentRepository(APPDbContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetAllAsync()
        {
            var AllStudent = await  _context.students
                .Include(e => e.enrollments)
                .ThenInclude(c => c.Course)
                .ToListAsync();
            return AllStudent;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            var GetStudntById = await _context.students
                .Include(e => e.enrollments)
                .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync( s => s.StudentId == id);
            if (GetStudntById == null)
            {
                return null;
            }
            else
            {
                return GetStudntById;
            }
        }

        public async Task<Student> AddAsync(Student student)
        {
            var AddStudent = await _context.students
                .AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }


        public async Task<Student?> UpdateAsync(int id, Student student)
        {
            var GetStudntById = await _context.students
                .Include(e => e.enrollments)
                .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync(s => s.StudentId == id);
            if (GetStudntById == null)
            {
                return null;
            }
            GetStudntById.StudentAge = student.StudentAge;
            GetStudntById.StudentName = student.StudentName;
            GetStudntById.Student_Email = student.Student_Email;
            GetStudntById.Student_Password = student.Student_Password;
            _context.enrollments.RemoveRange(GetStudntById.enrollments);
            
            if (student.enrollments != null)
            {
                foreach (var Enrolments in student.enrollments)
                {
                    GetStudntById.enrollments.Add(new Enrollment
                    {
                        StudentId = id,
                        CourseId = Enrolments.CourseId,
                        EnrollmentDate = DateTime.Now,
                    });
                }
                return GetStudntById;
            }
            await _context.SaveChangesAsync();
            return GetStudntById;
        }
        public async Task<bool?> DeleteByIdAsync(int id)
        {
            var GetStudntById = await _context.students
                .FirstOrDefaultAsync(s => s.StudentId == id);
            if (GetStudntById == null)
            {
                return null;
            }
            _context.students.RemoveRange(GetStudntById);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
