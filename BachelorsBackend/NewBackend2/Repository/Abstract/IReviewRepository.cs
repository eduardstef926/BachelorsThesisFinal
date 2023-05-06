
using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IReviewRepository
    {
        Task AddAppointmentReviewAsync(ReviewEntity review);
        Task<int> GetDoctorReviewLengthByFirstNameAndLastName(string firstName, string lastName);
        Task<List<int>> GetDoctorReviewNumbersByFirstNameAndLastName(string firstName, string lastName);    
        Task<List<ReviewEntity>> GetDoctorReviewsPaginatedByFirstNameAndLastName(string firstName, string lastName, int pageIndex);
    }
}
