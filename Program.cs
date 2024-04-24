using UescCoursesAPI.Infrastructure.Persistence;
using UescCoursesAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UescCoursesAPI.API.Endpoints;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using UescCoursesAPI.Services.Validators;
using UescCoursesAPI.Services.DTO;
using UescCoursesAPI.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Hosting.Builder;


var builder = WebApplication.CreateBuilder(args);

// configuração para ignorar referências cíclicas no Json
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "UescCoursesAPI",
            ValidAudience = "Common",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Chave secreta do projeto UescCoursesAPI"))
        };
    });
builder.Services.AddAuthorization();

// Add AppContext to DI
builder.Services.AddDbContext<UescCourseAPIContext>();

// Add Validators to DI
builder.Services.AddScoped<IValidator<UserPostDTO>, UserPostValidator>();

// Add Authenticator Manager to DI
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

//registrando os endpoints
app.RegisterCoursesEndpoints();
app.RegisterPedagogicProjectsEndpoints();
app.RegisterCurriculumsEndpoints();
app.RegisterDisciplinesEndpoints();
app.RegisterUsersEndpoints();
app.RegisterAuthEndpoints();

app.Run();
