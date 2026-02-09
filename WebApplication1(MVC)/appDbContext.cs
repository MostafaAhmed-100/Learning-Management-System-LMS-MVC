using Microsoft.EntityFrameworkCore;
using WebApplication1_MVC_.Entitys;

namespace WebApplication1_MVC_
{
    public class APPDbContext : DbContext
    {
        public APPDbContext(DbContextOptions<APPDbContext> options) : base(options)
        {
        }
        public DbSet<Student> students { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Instructor> instructors { get; set; }
        public DbSet<Enrollment> enrollments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });

        }
    }
}
