using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1_MVC_.Filter;
using WebApplication1_MVC_.Models;
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
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option => {
                option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
                option.Password.RequiredLength = 4;
                option.Password.RequireDigit = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<APPDbContext>()
            .AddRoles<IdentityRole>();  
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}")
                    .WithStaticAssets();

            app.Run();
        }
    }
}
