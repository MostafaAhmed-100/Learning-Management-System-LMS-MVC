using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Repositories.Implementation;
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
                InstractorName = c.Instructor?.InstructorName ?? "No Instructor For That Cousre",
                InstructorId = c.InstructorId,
            }).ToList();
        }
        public async Task<CourseResponseDTO?> GetCourseByIdAsync(int id)
        {
            var CourseId = await _courseRepository.GetByIdAsync(id);
            if (CourseId == null)
            {
                return null;
            }
            return new CourseResponseDTO
            {
                CourseId = CourseId.CourseId,
                CourseTitle = CourseId.CourseTitle,
                CourseDescription = CourseId.Description,
                CreatedDate = CourseId.CreatedDate,
                InstractorName = CourseId.Instructor?.InstructorName??"No Instructor For That Cousre",
                InstructorId = CourseId.CourseId,
                CoursePrice = CourseId.Price,
            };
        }
        public async Task<CourseResponseDTO?> UpdateCourseAsync(int id, CourseRequestDto course_request_dto)
        {
            var course = new Course
            {
                CourseId = id , 
                CourseTitle = course_request_dto.CourseTitle,
                Description = course_request_dto.CourseDescription,
                Price = course_request_dto.CoursePrice,
                InstructorId = course_request_dto.InstructorId,
            };
            var CourseId = await _courseRepository.UpdateAsync(id, course);
            if (CourseId == null)
            {
                return null;
            }
            var course_response_DTO = new CourseResponseDTO
            {
                CourseId = CourseId.CourseId,
                InstructorId = CourseId.InstructorId,
                CourseTitle = CourseId.CourseTitle,
                CourseDescription = CourseId.Description,
                CoursePrice = CourseId.Price,
                CreatedDate = CourseId.CreatedDate,
                InstractorName = CourseId.Instructor?.InstructorName?? "No Instructor For That Cousre",
            };
            return course_response_DTO;
        }
        public async Task<CourseResponseDTO> AddCourseAsync(CourseRequestDto course_request_dto)
        {
            var course = new Course
            {
                CourseId = course_request_dto.CourseId,
                CourseTitle = course_request_dto.CourseTitle,
                Description = course_request_dto.CourseDescription,
                Price = course_request_dto.CoursePrice,
                InstructorId = course_request_dto.InstructorId,
            };
            var AddCourse = await _courseRepository.AddAsync(course);
            var course_response_DTO = new CourseResponseDTO
            {
                CourseId = AddCourse.CourseId,
                CourseTitle = AddCourse.CourseTitle,
                CourseDescription = AddCourse.Description,
                CoursePrice= AddCourse.Price,
                CreatedDate = AddCourse.CreatedDate,
                InstractorName = AddCourse.Instructor?.InstructorName ?? "No Instructor For That Cousre",
            };
            return course_response_DTO;
        }
        public async Task<bool> DeleteCourseAsync(int id)
        {
            var DeleteCourse = await _courseRepository.DeleteAsync(id);
            if (DeleteCourse == null) 
            { return false; } 
            return true;
        }
    }
}