using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Repositories.Implementation;
using WebApplication1_MVC_.Repositories.Interface;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Service.Implementation
{
    public class EnrollmentService : IEnrollmentService
    {
        readonly IEnrollmentRepository _repository;
        public EnrollmentService(IEnrollmentRepository enrollmentRepository)
        {
            _repository = enrollmentRepository;
        }

        public async Task<List<EnrollmentResponseDTO>> GetAllEnrollmentsAsync()
        {
            var AllEnrolments = await _repository.GetAllAsync();
            return AllEnrolments.Select(x => new EnrollmentResponseDTO
            {
                CourseTitle = x.Course.CourseTitle,
                CoursePrice = x.Course.Price,
                EnrollmentDate = x.EnrollmentDate,
                StudentName = x.Student.StudentName,
            }).ToList();
        }
        public async Task<bool> EnrollStudentAsync(EnrollmentRequestDto enrollmentDto)
        {
            var Enrollment = new Enrollment
            {
                CourseId = enrollmentDto.CourseId,
                StudentId = enrollmentDto.StudentId,
            };
            var Enroll = await _repository.AddAsync(Enrollment);
            if (Enroll == null)
            {
                return false;
            }
            return true;
        }


        public async Task<bool> UnenrollStudentAsync(int studentId, int courseId)
        {
            var Unenroll =await _repository.DeleteByIdAsync(studentId, courseId);
            if (Unenroll == false)
                return false;
            return true;
        }
    }
}
