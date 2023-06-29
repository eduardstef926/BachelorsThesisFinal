using AutoMapper;
using NewBackend2.Dtos;
using NewBackend2.Jobs;
using NewBackend2.Model;
using NewBackend2.Repository;
using NewBackend2.Repository.Abstract;
using NewBackend2.Repository.Concrete;
using NewBackend2.Service.Abstract;
using NewBackend2.Service.Concrete;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<ProjectDatabaseConfiguration>(ServiceLifetime.Transient);
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<ICookieRepository, CookieRepository>();
builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IEmploymentRepository, EmploymentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDegreeRepository, DegreeRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IMedicalRepository, MedicalRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IHospitalService, HospitalService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();

    var jobKey = new JobKey("SymptomJob");
    q.AddJob<SymptomJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("SymptomJob-Trigger")
       // .WithCronSchedule("0/5 * * * * ?"));   // runs every 5 seconds
        .WithCronSchedule("0 0 * * * ?"));       // runs every day at midnight

    jobKey = new JobKey("AppointmentReminderJob");
    q.AddJob<AppointmentReminderJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("AppointmentReminderJob-Trigger")
       // .WithCronSchedule("0/5 * * * * ?"));
        .WithCronSchedule("0 0 * * * ?"));     

    jobKey = new JobKey("AppointmentFeedbackJob");
    q.AddJob<AppointmentFeedbackJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("AppointmentFeedbackJob-Trigger")
      //  .WithCronSchedule("0/5 * * * * ?"));
        .WithCronSchedule("0 0 * * * ?"));      
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<DoctorEntity, DoctorDto>();
    mc.CreateMap<DoctorDto, DoctorEntity>();
    mc.CreateMap<UserEntity, UserDto>();
    mc.CreateMap<UserEntity, FullUserDataDto>();
    mc.CreateMap<FullUserDataDto, UserEntity>();
    mc.CreateMap<UserDto, UserEntity>();
    mc.CreateMap<ReviewEntity, ReviewDto>();
    mc.CreateMap<DiseaseEntity, DiseaseDto>();
    mc.CreateMap<DiagnosisEntity, DiagnosisDto>();
    mc.CreateMap<HospitalEntity, HospitalDto>();
    mc.CreateMap<SubscriptionInputDto, SubscriptionEntity>();
    mc.CreateMap<SubscriptionEntity, SubscriptionDto>();
    mc.CreateMap<ReviewEntity, ReviewDto>();
    mc.CreateMap<SymptomEntity, SymptomDto>()
        .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Name));
    mc.CreateMap<ReviewDto, ReviewEntity>();
    mc.CreateMap<DegreeEntity, DegreeDto>()
        .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.College.Name))
        .ForMember(dto => dto.Location, opt => opt.MapFrom(src => src.College.Location))
        .ForMember(dto => dto.StartYear, opt => opt.MapFrom(src => src.StartYear))
        .ForMember(dto => dto.EndYear, opt => opt.MapFrom(src => src.EndYear))
        .ForMember(dto => dto.StudyProgram, opt => opt.MapFrom(src => src.StudyProgram));
    mc.CreateMap<EmploymentEntity, AppoimentSlotDto>()
        .ForMember(dto => dto.DoctorFirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
        .ForMember(dto => dto.DoctorLastName, opt => opt.MapFrom(src => src.Doctor.LastName))
        .ForMember(dto => dto.Location, opt => opt.MapFrom(src => src.Hospital.Location))
        .ForMember(dto => dto.HospitalName, opt => opt.MapFrom(src => src.HospitalName))
        .ForMember(dto => dto.StartTime, opt => opt.MapFrom(src => src.StartTime))
        .ForMember(dto => dto.EndTime, opt => opt.MapFrom(src => src.EndTime))
        .ForMember(dto => dto.Rating, opt => opt.MapFrom(src => src.Doctor.Rating))
        .ForMember(dto => dto.Price, opt => opt.MapFrom(src => src.ConsultPrice));
    mc.CreateMap<AppointmentEntity, AppointmentDto>()
        .ForMember(dto => dto.FirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
        .ForMember(dto => dto.LastName, opt => opt.MapFrom(src => src.Doctor.LastName))
        .ForMember(dto => dto.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate));
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowCredentials()
    .AllowAnyHeader()
    .WithOrigins("http://localhost:4200")
);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

