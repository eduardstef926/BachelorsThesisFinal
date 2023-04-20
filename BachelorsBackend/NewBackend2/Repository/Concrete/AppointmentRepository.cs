using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public AppointmentRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public async Task AddAppointmentAsync(AppointmentEntity appointment)
        {
            database.appointments.Add(appointment);
            await database.SaveChangesAsync();
        }

        public Task<AppointmentEntity> GetAppointmentByDateAsync(DateTime date)
        {
            return database.appointments.AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.AppointmentDate == date);
        }
    }
}
