using NewBackend2.Dtos;
using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IHospitalService
    {
        Task<List<HospitalDto>> GetAllHospitalsAsync();
    }
}
