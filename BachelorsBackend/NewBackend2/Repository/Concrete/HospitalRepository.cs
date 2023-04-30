using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public HospitalRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public Task<List<HospitalEntity>> GetAllHospitalsAsync()
        {
            return database.hospitals
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
