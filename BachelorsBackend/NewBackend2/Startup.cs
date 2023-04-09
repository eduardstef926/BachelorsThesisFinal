using AutoMapper;
using NewBackend2.Dtos;
using NewBackend2.Model;
using NewBackend2.Repository;
using NewBackend2.Repository.Abstract;
using NewBackend2.Repository.Concrete;
using NewBackend2.Service.Abstract;
using NewBackend2.Service.Concrete;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<ProjectDatabaseConfiguration>(ServiceLifetime.Transient);
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICoreService, CoreService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEngineerRepository, EngineerRepository>();
builder.Services.AddScoped<IEngineerService, EngineerService>();
builder.Services.AddScoped<IDegreeRepository, DegreeRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IUserSymptomMappingRepository, UserSymptomMappingRepository>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<DoctorEntity, DoctorDto>();
    mc.CreateMap<DoctorDto, DoctorEntity>();
    mc.CreateMap<UserEntity, UserDto>();
    mc.CreateMap<UserDto, UserEntity>();
    mc.CreateMap<EngineerDto, EngineerEntity>();
    mc.CreateMap<EngineerEntity, EngineerDto>();
    mc.CreateMap<ReviewEntity, ReviewDto>();
    mc.CreateMap<SymptomEntity, SymptomDto>()
            .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.Symptom));
    mc.CreateMap<ReviewDto, ReviewEntity>();
    mc.CreateMap<DegreeEntity, DegreeDto>()
            .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.College.Name))
            .ForMember(dto => dto.Location, opt => opt.MapFrom(src => src.College.Location))
            .ForMember(dto => dto.StartYear, opt => opt.MapFrom(src => src.StartYear))
            .ForMember(dto => dto.EndYear, opt => opt.MapFrom(src => src.EndYear))
            .ForMember(dto => dto.StudyProgram, opt => opt.MapFrom(src => src.StudyProgram));
    mc.CreateMap<ReviewEntity, ReviewDto>()
           .ForMember(dto => dto.Name, opt => opt.MapFrom(src => src.User.FirstName))
           .ForMember(dto => dto.Number, opt => opt.MapFrom(src => src.Number))
           .ForMember(dto => dto.Message, opt => opt.MapFrom(src => src.Message));
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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

