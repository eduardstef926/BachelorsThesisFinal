using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface IDoctorService
    {
        public Task AddDoctorAsync(DoctorDto doctor);
        public Task<List<DoctorDto>> GetAllDoctorsAsync();
    }
}
