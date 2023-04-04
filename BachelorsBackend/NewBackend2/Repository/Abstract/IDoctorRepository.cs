using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IDoctorRepository
    {
        Task AddDoctorAsync(DoctorEntity doctor);
        Task<List<DoctorEntity>> GetAllDoctorsAsync();
    }
}
