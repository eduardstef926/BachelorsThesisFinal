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

        public Task<List<EmploymentEntity>> GetAppointmentDatesByDateSpecializationAndLocation(DayOfWeek day, string location, string specialization)
        {
            return database.employments
                .Include(x => x.Hospital)
                .Include(x => x.Doctor)
                .Where(x => x.Hospital.Location == location && x.WeekDay == day && x.Doctor.Specialization == specialization)
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

        public Task<List<string>> GetDoctorLocationsByDoctorId(int doctorId)
        {
            return database.employments
                .Where(x => x.DoctorId == doctorId)
                .Select(x => x.Hospital.Location)
                .Distinct()
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<EmploymentEntity> GetEmploymentByDoctorIdAsync(int doctorId)
        {
            return database.employments
                .Where(x => x.DoctorId == doctorId)
                .Include(x => x.Hospital)
                .FirstOrDefaultAsync();
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
