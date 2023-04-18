using AutoMapper;
using NewBackend2.Dtos;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;

namespace NewBackend2.Service.Concrete
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IDegreeRepository degreeRepository;
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;

        public DoctorService(IDoctorRepository doctorRepository, IReviewRepository reviewRepository, IDegreeRepository doctorCollegeMappingRepository, IMapper mapper)
        {
            this.doctorRepository = doctorRepository;
            this.degreeRepository = doctorCollegeMappingRepository;
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
        }

        public async Task AddDoctorAsync(DoctorDto doctor)
        {
            await doctorRepository.AddDoctorAsync(mapper.Map<DoctorDto, DoctorEntity>(doctor));
        }

        public async Task<List<DoctorDto>> GetAllDoctorsAsync()
        {
            var doctors = await doctorRepository.GetAllDoctorsAsync();
            return doctors
                .Select(mapper.Map<DoctorEntity, DoctorDto>)
                .ToList();
        }

        public async Task<List<DegreeDto>> GetDoctorDegreeByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            var degrees = await degreeRepository.GetDegreeByFirstNameAndLastNameAsync(firstName, lastName);
            return degrees
                .Select(mapper.Map<DegreeEntity, DegreeDto>)
                .ToList();
        }

        public async Task<DoctorDto> GetDoctorByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            var doctor = await doctorRepository.GetDoctorByFirstNameAndLastNameAsync(firstName, lastName);
            return mapper.Map<DoctorEntity, DoctorDto>(doctor);
        }

        public async Task<List<ReviewDto>> GetDoctorReviewsByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            var reviews = await reviewRepository.GetDoctorReviewsByFirstNameAndLastName(firstName, lastName);
            return reviews
                .Select(mapper.Map<ReviewEntity, ReviewDto>)
                .ToList();
        }

        public async Task<List<DoctorDto>> GetDoctorsBySpecialization(string specialization)
        {
            var doctors = await doctorRepository.GetDoctorsBySpecializationAsync(specialization);

            return doctors
                .Select(mapper.Map<DoctorEntity, DoctorDto>)
                .ToList();
        }
    }
}
