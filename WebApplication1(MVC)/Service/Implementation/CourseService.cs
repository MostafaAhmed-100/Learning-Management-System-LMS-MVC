using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Repositories.Interface;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Service.Implementation
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<CourseResponseDTO>> GetAllCoursesAsync()
        {
            var AllCourses = await _courseRepository.GetAllAsync();
            return AllCourses.Select(c => new CourseResponseDTO
            {
                CourseId = c.CourseId,
                CourseTitle = c.CourseTitle,
                CreatedDate = c.CreatedDate,
                CourseDescription = c.Description,
                CoursePrice = c.Price,
                InstractorName = c.Instructor?.InstructorName ?? "لم يتم تحديد محاضر",
                InstructorId = c.InstructorId,
            }).ToList();
        }

        public async Task<CourseResponseDTO?> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return null;

            return new CourseResponseDTO
            {
                CourseId = course.CourseId,
                CourseTitle = course.CourseTitle,
                CourseDescription = course.Description,
                CreatedDate = course.CreatedDate,
                InstractorName = course.Instructor?.InstructorName ?? "لم يتم تحديد محاضر",
                InstructorId = course.InstructorId,
                CoursePrice = course.Price,
            };
        }

        public async Task<List<Instructor>> GetAllInstructorsAsync()
        {
            return await _courseRepository.GetAllInstructorsAsync();
        }

        public async Task<CourseResponseDTO?> UpdateCourseAsync(int id, CourseRequestDto courseDto)
        {
            var existingCourse = await _courseRepository.GetByIdAsync(id);
            if (existingCourse == null) return null;

            existingCourse.CourseTitle = courseDto.CourseTitle;
            existingCourse.Description = courseDto.CourseDescription;
            existingCourse.Price = courseDto.CoursePrice;
            existingCourse.InstructorId = courseDto.InstructorId;

            var updated = await _courseRepository.UpdateAsync(id, existingCourse);
            return await GetCourseByIdAsync(updated.CourseId);
        }

        public async Task<CourseResponseDTO> AddCourseAsync(CourseRequestDto courseDto)
        {
            var course = new Course
            {
                CourseTitle = courseDto.CourseTitle,
                Description = courseDto.CourseDescription,
                Price = courseDto.CoursePrice,
                InstructorId = courseDto.InstructorId,
                CreatedDate = DateTime.Now
            };

            var addedCourse = await _courseRepository.AddAsync(course);
            return await GetCourseByIdAsync(addedCourse.CourseId);
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var result = await _courseRepository.DeleteAsync(id);
            return result != null;
        }
    }
}