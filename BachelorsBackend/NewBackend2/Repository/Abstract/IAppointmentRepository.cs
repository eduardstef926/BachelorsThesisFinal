﻿using NewBackend2.Model;

namespace NewBackend2.Repository.Abstract
{
    public interface IAppointmentRepository
    {
        Task<bool> CheckAppointmentDateAsync(DateTime appointmentDate);
        Task AddAppointmentAsync(AppointmentEntity appointment);
        Task<List<AppointmentEntity>> GetFullAppointmentsDataAsync();
        Task<AppointmentEntity> GetAppointmentByDateAsync(DateTime date);
    }
}