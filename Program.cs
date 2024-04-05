using UescCoursesAPI.Infrastructure.Persistence;
using UescCoursesAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

var context = new UescCourseAPIContext(new DbContextOptions<UescCourseAPIContext>());

app.MapGet("/", () => "APIS de Cursos da UESC");

app.MapGet("/courses", () => context.Courses.ToList());

app.MapGet("/courses/{id}", (int id) => context.Courses.FirstOrDefault(c => c.CourseId == id));

app.MapPost("/courses", (Course course) => {
      context.Courses.Add(course);
      context.SaveChanges();
      return course;
});

app.MapPut("/courses/{id}", (int id, Course course) => {
      var courseToUpdate = context.Courses.FirstOrDefault(c => c.CourseId == id);
      courseToUpdate.Name = course.Name;
      courseToUpdate.Status = course.Status;
      context.SaveChanges();
      return courseToUpdate;
});

app.MapDelete("/courses/{id}", (int id) => {
      var courseToDelete = context.Courses.FirstOrDefault(c => c.CourseId == id);
      context.Courses.Remove(courseToDelete);
      context.SaveChanges();
      return courseToDelete;
});

app.MapGet("/courses/{id}/pedagogicProjects", (int id) => context.PedagogicProjects.Where(p => p.CourseId == id).ToList());

app.MapPost("/courses/{id}/pedagogicProjects", (int id, PedagogicProject proj) => {
      proj.CourseId = id;
      context.PedagogicProjects.Add(proj);
      context.SaveChanges();
      return proj;
});

app.Run();
