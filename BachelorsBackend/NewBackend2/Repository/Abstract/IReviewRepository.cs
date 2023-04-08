
using NewBackend2.Model;

namespace NewBackend2.Service.Abstract
{
    public interface IReviewRepository
    {
        Task<List<ReviewEntity>> GetDoctorReviewsByFirstNameAndLastName(string firstName, string lastName);
    }
}
