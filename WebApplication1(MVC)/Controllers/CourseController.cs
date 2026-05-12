using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Student,Instructor")]
        public async Task<IActionResult> All_Courses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return View(courses);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Student,Instructor")]
        public async Task<IActionResult> Course_Info(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add_Course()
        {
            var instructors = await _courseService.GetAllInstructorsAsync();
            ViewBag.Instructors = new SelectList(instructors, "InstructorId", "InstructorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add_Course(CourseRequestDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                var instructors = await _courseService.GetAllInstructorsAsync();
                ViewBag.Instructors = new SelectList(instructors, "InstructorId", "InstructorName");
                return View(courseDto);
            }

            await _courseService.AddCourseAsync(courseDto);
            return RedirectToAction(nameof(All_Courses));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update_Course(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            var instructors = await _courseService.GetAllInstructorsAsync();
            ViewBag.Instructors = new SelectList(instructors, "InstructorId", "InstructorName", course.InstructorId);

            return View(new CourseRequestDto
            {
                CourseTitle = course.CourseTitle,
                CourseDescription = course.CourseDescription,
                CoursePrice = course.CoursePrice,
                InstructorId = course.InstructorId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update_Course(int id, CourseRequestDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                var instructors = await _courseService.GetAllInstructorsAsync();
                ViewBag.Instructors = new SelectList(instructors, "InstructorId", "InstructorName", courseDto.InstructorId);
                return View(courseDto);
            }

            var result = await _courseService.UpdateCourseAsync(id, courseDto);
            if (result == null) return NotFound();

            return RedirectToAction(nameof(All_Courses));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete_Course(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return RedirectToAction(nameof(All_Courses));
        }
    }
}