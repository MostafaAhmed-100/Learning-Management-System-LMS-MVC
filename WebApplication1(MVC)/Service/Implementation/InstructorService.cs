using Microsoft.AspNetCore.Identity;
using WebApplication1_MVC_.DTOs;
using WebApplication1_MVC_.DTOs.Request_DTOs;
using WebApplication1_MVC_.Entitys;
using WebApplication1_MVC_.Repositories.Interface;
using WebApplication1_MVC_.Service.Interfaces;

namespace WebApplication1_MVC_.Service.Implementation
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _instructorRepository;

        private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();
        public InstructorService (IInstructorRepository instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }


        public async Task<List<InstructorResponseDTO>> GetAllInstructorsAsync()
        {
            var AllInstructors = await _instructorRepository.GetAllAsync();
            return AllInstructors.Select(x => new InstructorResponseDTO
            {
                InstructorName = x.InstructorName,
                InstructorEmail = x.InstructorEmail,
                InstructorPhone = x.InstructorPhone,
                InstructorBio = x.InstructorBio,
                CourseCount = x.courses?.Count?? 0 ,
            }).ToList();
        }

        public async Task<InstructorResponseDTO?> GetInstructorByIdAsync(int id)
        {
            var Instractor_By_Id = await _instructorRepository.GetByIdAsync(id);
            if (Instractor_By_Id == null)
            {
                return null;
            }
            var InstractorResponse = new InstructorResponseDTO
            {
                InstructorName = Instractor_By_Id.InstructorName,
                InstructorEmail = Instractor_By_Id.InstructorEmail,
                InstructorPhone = Instractor_By_Id.InstructorPhone,
                InstructorBio = Instractor_By_Id.InstructorBio,
                CourseCount = Instractor_By_Id.courses?.Count?? 0 ,
            };
            return InstractorResponse ;
        }

        public async Task<InstructorResponseDTO> AddInstructorAsync(InstructorRequestDTO instructor_requestDTO)
        {
            var Instractor = new Instructor
            {
                InstructorName = instructor_requestDTO.InstructorName,
                InstructorEmail = instructor_requestDTO.InstructorEmail,
                InstructorPhone = instructor_requestDTO.InstructorPhone,
                InstructorBio = instructor_requestDTO.InstructorBio,
                InstructorPassword = _passwordHasher.HashPassword( instructor_requestDTO.InstructorName , instructor_requestDTO.InstructorPassword),
            };
            var AddInstructor = await _instructorRepository.AddAsync(Instractor);
            var ResponseInstractor = new InstructorResponseDTO
            {
                InstructorName = AddInstructor.InstructorName,
                InstructorEmail = AddInstructor.InstructorEmail,
                InstructorPhone = AddInstructor.InstructorPhone,
                InstructorBio = AddInstructor.InstructorBio,
            };
            return ResponseInstractor ;
        }
        public async Task<InstructorResponseDTO?> UpdateInstructorAsync(int id, InstructorRequestDTO instructor_requestDTO)
        {
            var Instractor = new Instructor
            {
                InstructorId = id,
                InstructorName = instructor_requestDTO.InstructorName,
                InstructorEmail = instructor_requestDTO.InstructorEmail,
                InstructorPhone = instructor_requestDTO.InstructorPhone,
                InstructorBio = instructor_requestDTO.InstructorBio,
                InstructorPassword = _passwordHasher.HashPassword(instructor_requestDTO.InstructorName, instructor_requestDTO.InstructorPassword),
            };
            var UpdateInstructor = await _instructorRepository.UpdateById(id ,Instractor);
            if (UpdateInstructor == null)
            {
                return null;
            }
            var ResponseInstractor = new InstructorResponseDTO
            {
                InstructorName = UpdateInstructor.InstructorName,
                InstructorEmail = UpdateInstructor.InstructorEmail,
                InstructorPhone = UpdateInstructor.InstructorPhone,
                InstructorBio = UpdateInstructor.InstructorBio,
            };
            return ResponseInstractor;
        }

        public async Task<bool> DeleteInstructorAsync(int id)
        {
            var DeleteInstructor = await _instructorRepository.DeleteByIdAsync(id);
            if (DeleteInstructor == null)
                return false;
            return true;
        }
    }
}
