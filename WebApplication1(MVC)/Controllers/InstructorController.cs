using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> All_Instructors()
        {
            var instructors = await _instructorService.GetAllInstructorsAsync();
            return View(instructors);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Info_Instructor(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add_Instructor() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add_Instructor(InstructorRequestDTO instructorDto)
        {  
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
            {
                return View(instructorDto);
            }

            await _instructorService.AddInstructorAsync(instructorDto);
            return RedirectToAction(nameof(All_Instructors));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update_Instructor(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update_Instructor(int id, InstructorRequestDTO instructorDto)
        {
            if (!ModelState.IsValid) return View(instructorDto);
            var result = await _instructorService.UpdateInstructorAsync(id, instructorDto);
            if (result == null) return NotFound();
            return RedirectToAction(nameof(All_Instructors));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete_Instructor(int id)
        {
            await _instructorService.DeleteInstructorAsync(id);
            return RedirectToAction(nameof(All_Instructors));
        }
    }
}