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

// Database Configuration
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserIdentityAccessDbContext>(options => 
    options.UseSqlServer(connection));

// Dependency Injection Configuration
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IUserGroupService, UserGroupService>();
builder.Services.AddScoped<IGroupPermissionService, GroupPermissionService>();

// AutoMapper Configuration
builder.Services.AddAutoMapper(typeof(UserMappingProfile));
builder.Services.AddAutoMapper(typeof(GroupMappingProfile));
builder.Services.AddAutoMapper(typeof(PermissionMappingProfile));
builder.Services.AddAutoMapper(typeof(UserGroupMappingProfile));
builder.Services.AddAutoMapper(typeof(GroupPermissionMappingProfile));

// FluentValidation Configuration
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GroupValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PermissionValidator>();

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Configuration
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

// Middleware Setup
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(whiteListedOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();