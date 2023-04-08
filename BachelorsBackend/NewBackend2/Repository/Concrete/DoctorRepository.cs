﻿using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;
using NewBackend2.Repository.Abstract;

namespace NewBackend2.Repository.Concrete
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ProjectDatabaseConfiguration database;

        public DoctorRepository(ProjectDatabaseConfiguration database)
        {
            this.database = database;
        }

        public async Task AddDoctorAsync(DoctorEntity doctor)
        {
            database.doctors.Add(doctor);
            await database.SaveChangesAsync();
        }

        public Task<List<DoctorEntity>> GetAllDoctorsAsync()
        {
            return database.doctors
                .AsNoTracking()
                .ToListAsync();
        }
    }
}