using Microsoft.AspNetCore.Mvc;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;
        public InstructorController(IInstructorService instructorService) => _instructorService = instructorService;

        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorService.GetAllInstructorsAsync();
            return View(instructors);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstructorRequestDTO instructorDto)
        {
            if (!ModelState.IsValid) return View(instructorDto);
            await _instructorService.AddInstructorAsync(instructorDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await _instructorService.GetInstructorByIdAsync(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InstructorRequestDTO instructorDto)
        {
            if (!ModelState.IsValid) return View(instructorDto);
            var result = await _instructorService.UpdateInstructorAsync(id, instructorDto);
            if (result == null) return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}