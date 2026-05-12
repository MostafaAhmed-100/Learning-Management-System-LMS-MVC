using Microsoft.AspNetCore.Identity;
using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Models;
using WebApplication1_MVC_.Repositories.Interface;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Service.Implementation
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public InstructorService(IInstructorRepository instructorRepository, UserManager<ApplicationUser> userManager)
        {
            _instructorRepository = instructorRepository;
            _userManager = userManager;
        }

        public async Task<InstructorResponseDTO> AddInstructorAsync(InstructorRequestDTO instructorDto)
        {
            var identityUser = new ApplicationUser
            {
                UserName = instructorDto.InstructorEmail, 
                Email = instructorDto.InstructorEmail
            };

            var result = await _userManager.CreateAsync(identityUser, instructorDto.InstructorPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"فشل إنشاء حساب الهوية للمحاضر: {errors}");
            }

            await _userManager.AddToRoleAsync(identityUser, "Instructor");

            var instructor = new Instructor
            {
                InstructorName = instructorDto.InstructorName,
                InstructorEmail = instructorDto.InstructorEmail,
                InstructorPhone = instructorDto.InstructorPhone,
                InstructorBio = instructorDto.InstructorBio,
                IdentityUserId = identityUser.Id 
            };

            var addedInstructor = await _instructorRepository.AddAsync(instructor);

            return new InstructorResponseDTO
            {
                InstracrorId = addedInstructor.InstructorId,
                InstructorName = addedInstructor.InstructorName,
                InstructorEmail = addedInstructor.InstructorEmail,
                InstructorPhone = addedInstructor.InstructorPhone,
                InstructorBio = addedInstructor.InstructorBio
            };
        }

        public async Task<List<InstructorResponseDTO>> GetAllInstructorsAsync()
        {
            var instructors = await _instructorRepository.GetAllAsync();
            return instructors.Select(i => new InstructorResponseDTO
            {
                InstracrorId = i.InstructorId,
                InstructorName = i.InstructorName,
                InstructorEmail = i.InstructorEmail,
                InstructorPhone = i.InstructorPhone,
                InstructorBio = i.InstructorBio,
                Coures = i.courses?.Select(c => c.CourseTitle).ToList() ?? new List<string>()
            }).ToList();
        }

        public async Task<InstructorResponseDTO?> GetInstructorByIdAsync(int id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);
            if (instructor == null) return null;

            return new InstructorResponseDTO
            {
                InstracrorId = instructor.InstructorId,
                InstructorName = instructor.InstructorName,
                InstructorEmail = instructor.InstructorEmail,
                InstructorPhone = instructor.InstructorPhone,
                InstructorBio = instructor.InstructorBio,
                Coures = instructor.courses?.Select(c => c.CourseTitle).ToList() ?? new List<string>()
            };
        }

        public async Task<InstructorResponseDTO?> UpdateInstructorAsync(int id, InstructorRequestDTO instructorDto)
        {
            var existingInstructor = await _instructorRepository.GetByIdAsync(id);
            if (existingInstructor == null) return null;

            existingInstructor.InstructorName = instructorDto.InstructorName;
            existingInstructor.InstructorEmail = instructorDto.InstructorEmail;
            existingInstructor.InstructorPhone = instructorDto.InstructorPhone;
            existingInstructor.InstructorBio = instructorDto.InstructorBio;

            // تحديث الباسوورد في الـ Identity لو مبعوت
            if (!string.IsNullOrEmpty(instructorDto.InstructorPassword) && instructorDto.InstructorPassword != "********")
            {
                var user = await _userManager.FindByIdAsync(existingInstructor.IdentityUserId);
                if (user != null)
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, instructorDto.InstructorPassword);
                }
            }

            var updated = await _instructorRepository.UpdateById(id, existingInstructor);
            return await GetInstructorByIdAsync(updated.InstructorId);
        }

        public async Task<bool> DeleteInstructorAsync(int id)
        {
            var instructor = await _instructorRepository.GetByIdAsync(id);
            if (instructor == null) return false;

            if (!string.IsNullOrEmpty(instructor.IdentityUserId))
            {
                var user = await _userManager.FindByIdAsync(instructor.IdentityUserId);
                if (user != null) await _userManager.DeleteAsync(user);
            }

            await _instructorRepository.DeleteByIdAsync(id);
            return true;
        }
    }
}