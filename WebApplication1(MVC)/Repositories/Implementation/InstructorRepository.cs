using Microsoft.EntityFrameworkCore;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Repositories.Interface;

namespace WebApplication1_MVC_.Repositories.Implementation
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly APPDbContext _context;

        public InstructorRepository(APPDbContext context)
        {
            _context = context;
        }

        public async Task<List<Instructor>> GetAllAsync()
        {
            return await _context.instructors
                .Include(i => i.courses)
                .ToListAsync();
        }

        public async Task<Instructor?> GetByIdAsync(int Id)
        {
            return await _context.instructors
                .Include(i => i.courses)
                .FirstOrDefaultAsync(i => i.InstructorId == Id);
        }

        public async Task<Instructor> AddAsync(Instructor instructor)
        {
            await _context.instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
            return instructor;
        }

        public async Task<Instructor?> UpdateById(int Id, Instructor instructor)
        {
            var existingInstructor = await _context.instructors.FirstOrDefaultAsync(i => i.InstructorId == Id);

            if (existingInstructor == null) return null;

            existingInstructor.InstructorName = instructor.InstructorName;
            existingInstructor.InstructorEmail = instructor.InstructorEmail;
            existingInstructor.InstructorPhone = instructor.InstructorPhone;
            existingInstructor.InstructorBio = instructor.InstructorBio;
            existingInstructor.InstructorPassword = instructor.InstructorPassword;

            await _context.SaveChangesAsync();
            return existingInstructor;
        }

        public async Task<bool?> DeleteByIdAsync(int Id)
        {
            var instructor = await _context.instructors.FirstOrDefaultAsync(i => i.InstructorId == Id);

            if (instructor == null) return null;

            _context.instructors.Remove(instructor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}