using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Models;
using WebApplication1_MVC_.Repositories.Interface;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Service.Implementation
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repository;
        private readonly IStudentRepository _studentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = enrollmentRepository;
            _studentRepository = studentRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<EnrollmentResponseDTO>> GetAllEnrollmentsAsync()
        {
            var AllEnrolments = await _repository.GetAllAsync();
            return AllEnrolments.Select(x => new EnrollmentResponseDTO
            {
                CourseTitle = x.Course.CourseTitle,
                CoursePrice = x.Course.Price,
                CourseId = x.Course.CourseId,
                EnrollmentDate = x.EnrollmentDate,
                StudentName = x.Student.StudentName,
                StudentId = x.Student.StudentId,
            }).ToList();
        }

        public async Task<bool> EnrollStudentAsync(EnrollmentRequestDto enrollmentDto)
        {

            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId)) return false;
            var student = await _studentRepository.GetAllAsync();
            var currentStudent = student.FirstOrDefault(s => s.IdentityUserId == userId);

            if (currentStudent == null) return false;


            var enrollment = new Enrollment
            {
                CourseId = enrollmentDto.CourseId,
                StudentId = currentStudent.StudentId, 
                EnrollmentDate = DateTime.Now,
            };

            var result = await _repository.AddAsync(enrollment);
            return result != null;
        }

        public async Task<bool?> UnenrollStudentAsync(int studentId, int courseId)
        {
            var result = await _repository.DeleteByIdAsync(studentId, courseId);
            return result;
        }
    }
}