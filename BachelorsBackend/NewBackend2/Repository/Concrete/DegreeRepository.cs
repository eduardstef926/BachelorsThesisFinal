using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class DegreeRepository : IDegreeRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public DegreeRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public Task<List<DegreeEntity>> GetDegreeByFirstNameAndLastNameAsync(string firstName, string lastName)
        {
            return database.degrees
                .Where(x => x.Doctor.FirstName == firstName && x.Doctor.LastName == lastName)
                .Include(x => x.College)
                .OrderByDescending(x => x.StartYear)
                .ToListAsync();
                
        }
    }
}
