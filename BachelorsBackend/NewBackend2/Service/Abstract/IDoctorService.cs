﻿using NewBackend2.Dtos;

namespace NewBackend2.Service.Abstract
{
    public interface IDoctorService
    {
        Task AddDoctorAsync(DoctorDto doctor);
        Task<List<DoctorDto>> GetAllDoctorsAsync();
        Task<List<AppoimentSlotDto>> GetAppointmentDatesByDateSpecializationAndLocationAsync(string startDate, string endDate, string location, string specialization);
        Task<List<string>> GetDoctorLocationsBySpecializationAsync(string specialization);
        Task<List<DegreeDto>> GetDoctorDegreeByFirstNameAndLastNameAsync(string firstName, string lastName);
        Task<List<ReviewDto>> GetDoctorReviewsByFirstNameAndLastNameAsync(string firstName, string lastName);
        Task<DoctorDto> GetDoctorWithEmploymentByFirstNameAndLastNameAsync(string firstName, string lastName);
        Task<List<DoctorDto>> GetDoctorsBySpecialization(string specialization);
    }
}
