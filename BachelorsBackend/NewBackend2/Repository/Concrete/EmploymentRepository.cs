using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class EmploymentRepository : IEmploymentRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public EmploymentRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public Task<List<EmploymentEntity>> GetAppointmentSlotsByDayAndLocationAsync(DayOfWeek day,string location)
        {
            return database.employments
                .Include(x => x.Hospital)
                .Include(x => x.Doctor)
                .Where(x => x.Hospital.Location == location && x.WeekDay == day)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<string>> GetDoctorLocationsBySpecializationAsync(string specialization)
        {
            return database.employments
                .Include(x => x.Doctor)
                .Where(x => x.Doctor.Specialization == specialization)
                .Select(x => x.Hospital.Location)
                .Distinct()
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<UserEntity> GetUserByAppointmentAsync(AppointmentEntity appointmentEntity)
        {
            return database.appointments
                .Where(x => x.AppointmentId == appointmentEntity.AppointmentId)
                .Select(x => x.User)
                .FirstOrDefaultAsync();   
        }
    }
}
