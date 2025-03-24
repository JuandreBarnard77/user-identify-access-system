
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using UserIdentityAccess.Application.Interfaces;
using UserIdentityAccess.Application.Mappings;
using UserIdentityAccess.Application.Validators;
using UserIdentityAccess.Application.Services;
using UserIdentityAccess.Infrastructure.Persistence.Data;
using UserIdentityAccess.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserIdentityAccessDbContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddAutoMapper(typeof(UserMappingProfile)); 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
var whiteListedOrigins = "AllowedOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: whiteListedOrigins,
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(whiteListedOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();