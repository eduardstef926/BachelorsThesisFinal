using Microsoft.EntityFrameworkCore;
using NewBackend2.Model;

namespace NewBackend2.Repository
{
    public class ProjectDatabaseConfiguration : DbContext
    {
        private readonly DbContextOptions<ProjectDatabaseConfiguration> _options;

        public DbContextOptions<ProjectDatabaseConfiguration> Options
        {
            get
            {
                return _options;
            }
        }

        public DbSet<UserEntity> users { get; set; }
        public DbSet<SymptomEntity> symptoms { get; set; }
        public DbSet<AppointmentEntity> appointments { get; set; }
        public DbSet<DoctorEntity> doctors { get; set; }
        public DbSet<EmailEntity> emails { get; set; }
        public DbSet<DegreeEntity> degrees { get; set; }
        public DbSet<ReviewEntity> reviews { get; set; }
        public DbSet<DiagnosticEntity> diagnostics { get; set; }
        public DbSet<DiseaseEntity> diseases { get; set; }
        public DbSet<EmploymentEntity> employments { get; set; }
        public DbSet<SubscriptionEntity> subscriptions { get; set; }
        public DbSet<CookiesEntity> cookies { get; set; }
        public DbSet<HospitalEntity> hospitals { get; set; }

        public ProjectDatabaseConfiguration(DbContextOptions<ProjectDatabaseConfiguration> options) : base(options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=DESKTOP-VUU0K4S;Database=CollegeLicense;Trusted_Connection=true;")
                .EnableSensitiveDataLogging();
        }

    }
}
