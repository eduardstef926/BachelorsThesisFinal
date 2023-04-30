using AutoMapper;
using NewBackend2.Dtos;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;
using NewBackend2.Service.Abstract;

namespace NewBackend2.Service.Concrete
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository hospitalRepository;
        private readonly IMapper mapper;

        public HospitalService(IHospitalRepository hospitalRepository, IMapper mapper)
        {
            this.hospitalRepository = hospitalRepository;
            this.mapper = mapper;
        }

        public async Task<List<HospitalDto>> GetAllHospitalsAsync()
        {
           var hospitals = await hospitalRepository.GetAllHospitalsAsync();
           return hospitals
                .Select(mapper.Map<HospitalEntity, HospitalDto>)
                .ToList();
        }
    }
}
