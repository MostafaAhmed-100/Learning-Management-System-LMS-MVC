using Microsoft.AspNetCore.Mvc;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService) => _studentService = studentService;
        [HttpGet]
        public async Task<IActionResult> All_Students()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Update_Student(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();

            var updateModel = new StudentRequestDto
            {
                StudentName = student.StudentName,
                StudentEmail = student.Student_Email,
                StudentAge = student.StudentAge,
                StudentPassword = "****************"
            };
            return View(updateModel);
        }
       [HttpPost]
        public async Task<IActionResult> Update_Student(int id, StudentRequestDto studentDto)
        {
            if (!ModelState.IsValid) return View(studentDto);

            var result = await _studentService.UpdateStudentAsync(id, studentDto);
            if (result == null) return NotFound();

            return RedirectToAction(nameof(All_Students));
        }
        [HttpGet]
        public IActionResult Add_Student()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add_Student(StudentRequestDto studentDto)
        {
            if (!ModelState.IsValid) return View(studentDto);
            await _studentService.AddStudentAsync(studentDto);
            return RedirectToAction(nameof(All_Students));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(All_Students));
        }
    }
}
