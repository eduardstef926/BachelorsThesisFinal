using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository;
using NewBackend2.Service.Abstract;

namespace NewBackend2.Service.Concrete
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public ReviewRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public async Task<List<ReviewEntity>> GetDoctorReviewsByFirstNameAndLastName(string firstName, string lastName)
        {
            return database.reviews
                .Where(x => x.Doctor.FirstName == firstName && x.Doctor.LastName == lastName)
                .Include(x => x.User)
                .ToList();
        }
    }
}
