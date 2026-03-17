using Microsoft.AspNetCore.Mvc;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService; // عشان نجيب أسامي الطلاب
        private readonly ICourseService _courseService;   // عشان نجيب أسامي الكورسات

        public EnrollmentController(
            IEnrollmentService enrollmentService,
            IStudentService studentService,
            ICourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            return View(enrollments);
        }

        // 2. صفحة التسجيل (GET) - "ركز هنا"
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var students = await _studentService.GetAllStudentsAsync();
            var courses = await _courseService.GetAllCoursesAsync();
            ViewBag.Students = students;
            ViewBag.Courses = courses;

            return View();
        }

        // 3. تنفيذ التسجيل (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnrollmentRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Students = await _studentService.GetAllStudentsAsync();
                ViewBag.Courses = await _courseService.GetAllCoursesAsync();
                return View(dto);
            }

            var result = await _enrollmentService.EnrollStudentAsync(dto);
            if (!result)
            {
                ModelState.AddModelError("", "هذا الطالب مسجل بالفعل في هذا الكورس!");
                ViewBag.Students = await _studentService.GetAllStudentsAsync();
                ViewBag.Courses = await _courseService.GetAllCoursesAsync();
                return View(dto);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int studentId, int courseId)
        {
            await _enrollmentService.UnenrollStudentAsync(studentId, courseId);
            return RedirectToAction(nameof(Index));
        }
    }
}
