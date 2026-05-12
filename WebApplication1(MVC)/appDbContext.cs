using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Models;

namespace WebApplication1_MVC_
{
    public class APPDbContext : IdentityDbContext<ApplicationUser>
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
