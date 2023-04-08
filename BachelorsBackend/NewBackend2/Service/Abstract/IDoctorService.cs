using NewBackend2.Dtos;
using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IDoctorService
    {
        public Task AddDoctorAsync(DoctorDto doctor);
        public Task<List<DoctorDto>> GetAllDoctorsAsync();
        public Task<List<DegreeDto>> GetDoctorDegreeByFirstNameAndLastNameAsync(string firstName, string lastName);
        public Task<List<ReviewDto>> GetDoctorReviewsByFirstNameAndLastNameAsync(string firstName, string lastName);
        public Task<DoctorDto> GetDoctorByFirstNameAndLastNameAsync(string firstName, string lastName);
    }
}
