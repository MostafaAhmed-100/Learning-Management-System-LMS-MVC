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
        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return View(students);
        }
        // بيفتح الصفحة ببيانات الطالب القديمة
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        //بيستقبل البيانات المتعدلة ويحفظها
        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentRequestDto studentDto)
        {
            if (!ModelState.IsValid) return View(studentDto);

            var result = await _studentService.UpdateStudentAsync(id, studentDto);
            if (result == null) return NotFound();

            return RedirectToAction(nameof(Index)); // ارجع للجدول بعد التعديل
        }
        [HttpGet]
        public IActionResult Create()
        {
           return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentRequestDto studentDto)
        {
            if (!ModelState.IsValid) return View(studentDto);
            await _studentService.AddStudentAsync(studentDto);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
