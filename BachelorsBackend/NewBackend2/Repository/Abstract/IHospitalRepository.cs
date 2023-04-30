using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IHospitalRepository
    {
        Task<List<HospitalEntity>> GetAllHospitalsAsync();
    }
}
