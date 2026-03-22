using Microsoft.AspNetCore.Identity;
using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Repositories.Interface;
using WebApplication1_MVC_.Service.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1_MVC_.Service.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();

        readonly IStudentRepository _studentRepository;

        public StudentService (IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
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
                Enrollments = x.enrollments.Select(e => e.Course.CourseTitle).ToList()
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
        public async Task<StudentResponseDTO> AddStudentAsync(StudentRequestDto student_requestDto)
        {
            var Student = new Student
            {
                StudentName = student_requestDto.StudentName,
                Student_Email = student_requestDto.StudentEmail,
                StudentAge = student_requestDto.StudentAge,
                Student_Password = _passwordHasher.HashPassword(student_requestDto.StudentName , student_requestDto.StudentPassword)
            };
            var Add_Student = await _studentRepository.AddAsync(Student);
            var student_responseDTO = new StudentResponseDTO
            {
                StudentName = Add_Student.StudentName,
                StudentAge = Add_Student.StudentAge,
                Student_Email = Add_Student.Student_Email,
            };
            return student_responseDTO;
        }
        public async Task<StudentResponseDTO?> UpdateStudentAsync(int id, StudentRequestDto student_requestDto)
        {

            var existingStudent = await _studentRepository.GetByIdAsync(id);
            if (existingStudent == null) return null;
            existingStudent.StudentId = id;
            existingStudent.StudentName = student_requestDto.StudentName;
            existingStudent.Student_Email = student_requestDto.StudentEmail;
            existingStudent.StudentAge = student_requestDto.StudentAge;
            if (!string.IsNullOrEmpty(student_requestDto.StudentPassword) && student_requestDto.StudentPassword != "****************")
            {
                string StrId = Convert.ToString(id);
                existingStudent.Student_Password = _passwordHasher.HashPassword(StrId, student_requestDto.StudentPassword);
            }
            var Student_Update = await _studentRepository.UpdateAsync(id, existingStudent);

            return new StudentResponseDTO
            {
                StudentName = Student_Update.StudentName,
                StudentAge = Student_Update.StudentAge,
                Student_Email = Student_Update.Student_Email
            };
        }
        public async Task<bool> DeleteStudentAsync(int id)
        {
            var Student_Delete = await _studentRepository.DeleteByIdAsync(id);
            if (Student_Delete == null)
                return false;
            return true;
        }
    }
}
