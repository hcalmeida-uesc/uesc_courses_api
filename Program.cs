using UescCoursesAPI.Infrastructure.Persistence;
using UescCoursesAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using UescCoursesAPI.API.Endpoints;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using UescCoursesAPI.Validators;


var builder = WebApplication.CreateBuilder(args);

// configuração para ignorar referências cíclicas no Json
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add AppContext to DI
builder.Services.AddDbContext<UescCourseAPIContext>();

// Add Validators to DI
builder.Services.AddScoped<IValidator<User>, UserValidator>();

var app = builder.Build();

// não devemos criar o contexto aqui, pois ele não é thread-safe
// a cada requisição, um novo contexto deve ser criado
// Melhor criar o contexto dentro de cada rota
// ou utilizar Injeção de Dependência
//var context = new UescCourseAPIContext(new DbContextOptions<UescCourseAPIContext>());

//registrando os endpoints
app.RegisterCoursesEndpoints();
app.RegisterPedagogicProjectsEndpoints();
app.RegisterCurriculumsEndpoints();
app.RegisterDisciplinesEndpoints();
app.RegisterUsersEndpoints();

app.Run();
