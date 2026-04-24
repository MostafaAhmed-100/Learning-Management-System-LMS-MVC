using Microsoft.AspNetCore.Mvc;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public EnrollmentController(
            IEnrollmentService enrollmentService,
            IStudentService studentService,
            ICourseService courseService)
        {
            _enrollmentService = enrollmentService;
            _studentService = studentService;
            _courseService = courseService;
        }

        public async Task<IActionResult> All_Enrollment()
        {
            var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
            return View(enrollments);
        }
        [HttpGet]
        public async Task<IActionResult> Add_Enrollment()
        {
            ViewBag.Students = await _studentService.GetAllStudentsAsync();
            ViewBag.Courses = await _courseService.GetAllCoursesAsync();
            var dto = new EnrollmentRequestDto();

            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Add_Enrollment(EnrollmentRequestDto dto)
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

            return RedirectToAction(nameof(All_Enrollment));
        }
        [HttpPost]
        public async Task<IActionResult> Delete_Enrollment(int studentId, int courseId)
        {
            await _enrollmentService.UnenrollStudentAsync(studentId, courseId);
            return RedirectToAction(nameof(All_Enrollment));
        }
    }
}
