using Microsoft.AspNetCore.Identity;
using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Models;
using WebApplication1_MVC_.Repositories.Interface;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Service.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentService(IStudentRepository studentRepository, UserManager<ApplicationUser> userManager)
        {
            _studentRepository = studentRepository;
            _userManager = userManager;
        }

        public async Task<StudentResponseDTO> AddStudentAsync(StudentRequestDto studentDto)
        {
            var identityUser = new ApplicationUser
            {
                UserName = studentDto.StudentEmail,
                Email = studentDto.StudentEmail
            };

            var result = await _userManager.CreateAsync(identityUser, studentDto.StudentPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"فشل إنشاء الحساب: {errors}");
            }

            var student = new Student
            {
                StudentName = studentDto.StudentName,
                Student_Email = studentDto.StudentEmail,
                StudentAge = studentDto.StudentAge,
                IdentityUserId = identityUser.Id
                
            };

            var addedStudent = await _studentRepository.AddAsync(student);

            return new StudentResponseDTO
            {
                StudentId = addedStudent.StudentId,
                StudentName = addedStudent.StudentName,
                Student_Email = addedStudent.Student_Email,
                StudentAge = addedStudent.StudentAge,
                Student_Password = "****************"
            };
        }

        public async Task<List<StudentResponseDTO>> GetAllStudentsAsync()
        {
            var All_Students = await _studentRepository.GetAllAsync();
            return All_Students.Select(x => new StudentResponseDTO
            {
                StudentId = x.StudentId,
                StudentName = x.StudentName,
                Student_Email = x.Student_Email,
                StudentAge = x.StudentAge,
                Enrollments = x.enrollments?.Select(e => e.Course.CourseTitle).ToList() ?? new List<string>()
            }).ToList();
        }

        public async Task<StudentResponseDTO?> GetStudentByIdAsync(int id)
        {
            var Stu_by_Id = await _studentRepository.GetByIdAsync(id);
            if (Stu_by_Id == null)
                return null;
            return new StudentResponseDTO
            {
                StudentId = Stu_by_Id.StudentId,
                StudentName = Stu_by_Id.StudentName,
                StudentAge = Stu_by_Id.StudentAge,
                Student_Email = Stu_by_Id.Student_Email,
                Enrollments = Stu_by_Id.enrollments.Select(e => e.Course.CourseTitle).ToList()
            };
        }
        public async Task<StudentResponseDTO?> UpdateStudentAsync(int id, StudentRequestDto studentDto)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(id);
            if (existingStudent == null) return null;
            existingStudent.StudentName = studentDto.StudentName;
            existingStudent.Student_Email = studentDto.StudentEmail;
            existingStudent.StudentAge = studentDto.StudentAge;

            if (!string.IsNullOrEmpty(studentDto.StudentPassword) && studentDto.StudentPassword != "****************")
            {
                var user = await _userManager.FindByIdAsync(existingStudent.IdentityUserId);
                if (user != null)
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, studentDto.StudentPassword);
                }
            }   

            var updatedStudent = await _studentRepository.UpdateAsync(id, existingStudent);

            return new StudentResponseDTO
            {
                StudentId = updatedStudent.StudentId,
                StudentName = updatedStudent.StudentName,
                StudentAge = updatedStudent.StudentAge,
                Student_Email = updatedStudent.Student_Email
            };
        }
        public async Task<bool> DeleteStudentAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return false;

            if (!string.IsNullOrEmpty(student.IdentityUserId))
            {
                var user = await _userManager.FindByIdAsync(student.IdentityUserId);
                if (user != null) await _userManager.DeleteAsync(user);
            }
            await _studentRepository.DeleteByIdAsync(id);
            return true;
        }
    }
}