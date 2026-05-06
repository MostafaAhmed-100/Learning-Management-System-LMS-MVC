using Microsoft.EntityFrameworkCore;
using WebApplication1_MVC_.Filter;
using WebApplication1_MVC_.Repositories.Implementation;
using WebApplication1_MVC_.Repositories.Interface;
using WebApplication1_MVC_.Service.Implementation;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new MyCustomExceptionFilterAttribute());
            });
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<APPDbContext>(options =>
                    options.UseSqlServer(connectionString));
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
            builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            builder.Services.AddScoped<ICourseService , CourseService>();
            builder.Services.AddScoped<IStudentService , StudentService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
            var app = builder.Build();

            
            app.UseRouting();
            // Logging for Requests

            app.Use(async (HttpContext, next) =>
            {
                var path = HttpContext.Request.Path.ToString().ToLower();
                string[] staticExtensions = { ".css", ".js", ".png", ".jpg", ".svg", ".ico" };
                if (!staticExtensions.Any(SE =>  path.EndsWith(SE)))
                {
                    Console.WriteLine("===============================================================================================");
                    var method = HttpContext.Request.Method;
                    Console.WriteLine($"The method in use is :{method} \n" +
                        $"and the path is : {path} ");
                    Console.WriteLine("===============================================================================================");
                }
                await next();
            });

            app.UseAuthorization();

            app.MapStaticAssets();
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Student}/{action=All_Students}/{id?}")
                    .WithStaticAssets();

            app.Run();
        }
    }
}
