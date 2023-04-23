﻿using Microsoft.EntityFrameworkCore;
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

        public Task<List<AppointmentEntity>> GetFullAppointmentsDataAsync()
        {
            return database.appointments
                .Include(x => x.User)
                .Include(x => x.Doctor)
                .ToListAsync();
        }

        public async Task AddAppointmentAsync(AppointmentEntity appointment)
        {
            database.appointments.Add(appointment);
            await database.SaveChangesAsync();
        }

        public async Task<bool> CheckAppointmentDateAsync(DateTime appointmentDate)
        {
            return database.appointments
                .Any(x => x.AppointmentDate == appointmentDate);    
        }

        public Task<AppointmentEntity> GetAppointmentByDateAsync(DateTime date)
        {
            return database.appointments.AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.AppointmentDate == date);
        }

        public Task<AppointmentEntity> GetAppointmentByIdAsync(int id)
        {
            return database.appointments.AsNoTracking()
                .Include(x => x.Doctor)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.AppointmentId == id);
        }
    }
}
