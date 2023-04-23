
using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IReviewRepository
    {
        Task AddAppointmentReviewAsync(ReviewEntity review);
        Task<List<int>> GetDoctorReviewNumbersByFirstNameAndLastName(string firstName, string lastName);    
        Task<List<ReviewEntity>> GetDoctorReviewsByFirstNameAndLastName(string firstName, string lastName);
    }
}
