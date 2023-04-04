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
builder.Services.AddScoped<ISymptomRespository, SymptomRepository>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<DoctorEntity, DoctorDto>();
    mc.CreateMap<DoctorDto, DoctorEntity>();
    mc.CreateMap<UserEntity, UserDto>();
    mc.CreateMap<UserDto, UserEntity>();
    mc.CreateMap<EngineerDto, EngineerEntity>();
    mc.CreateMap<EngineerEntity, EngineerDto>();
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

