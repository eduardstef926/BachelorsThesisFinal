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
        private readonly IMapper mapper;

        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper)
        {
            this.doctorRepository = doctorRepository;
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
    }
}
