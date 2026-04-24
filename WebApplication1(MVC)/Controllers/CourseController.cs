using Microsoft.AspNetCore.Mvc;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService) => _courseService = courseService;
        [HttpGet]
        public async Task<IActionResult> All_Courses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return View(courses);
        }
        [HttpGet]
        public async Task<IActionResult> Course_Info(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }
        [HttpGet]
        public IActionResult Add_Course () => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Course(CourseRequestDto courseDto)
        {
            if (!ModelState.IsValid) return View(courseDto);
            await _courseService.AddCourseAsync(courseDto);
            return RedirectToAction(nameof(All_Courses));
        }
        [HttpGet]
        public async Task<IActionResult> Update_Course(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update_Course(int id, CourseRequestDto courseDto)
        {
            if (!ModelState.IsValid) return View(courseDto);
            var result = await _courseService.UpdateCourseAsync(id, courseDto);
            if (result == null) return NotFound();
            return RedirectToAction(nameof(All_Courses));
        }
        [HttpPost]
        public async Task<IActionResult> Delete_Course(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(All_Courses));
        }
    }
}
