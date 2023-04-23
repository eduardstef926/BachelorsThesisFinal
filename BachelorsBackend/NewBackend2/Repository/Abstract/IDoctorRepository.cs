using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IDoctorRepository
    {
        Task AddDoctorAsync(DoctorEntity doctor);
        Task UpdateDoctorEvaluationNumberAsync(int id, float newNumber);
        Task<List<DoctorEntity>> GetAllDoctorsAsync();
        Task<int> GetDoctorIdByFirstNameAndLastNameAsync(string firstName, string lastName);  
        Task<DoctorEntity> GetDoctorByFirstNameAndLastNameAsync(string firstName, string lastName);
        Task<List<DoctorEntity>> GetDoctorsBySpecializationAsync(string specialization);
    }
}
