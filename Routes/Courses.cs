using UescCoursesAPI.Domain;
using UescCoursesAPI.Infrastructure.Persistence;

namespace UescCoursesAPI.API.Endpoints;

public static class Courses
{
   public static void RegisterCoursesEndpoints(this IEndpointRouteBuilder routes)
   {
      var coursesRoutes = routes.MapGroup("/courses");

      coursesRoutes.MapGet("", (UescCourseAPIContext context) => context.Courses.ToList());

      coursesRoutes.MapGet("/{id}", (int id, UescCourseAPIContext context) => context.Courses.FirstOrDefault(c => c.CourseId == id));

      coursesRoutes.MapPost("", (Course course,UescCourseAPIContext context) =>
      {
         context.Courses.Add(course);
         context.SaveChanges();
         return course;
      });

      coursesRoutes.MapPut("/{id}", (int id, Course course, UescCourseAPIContext context) =>
      {
         var courseToUpdate = context.Courses.FirstOrDefault(c => c.CourseId == id);
         courseToUpdate.Name = course.Name;
         courseToUpdate.Status = course.Status;
         context.SaveChanges();
         return courseToUpdate;
      });

      coursesRoutes.MapDelete("/{id}", (int id, UescCourseAPIContext context) =>
      {
         var courseToDelete = context.Courses.FirstOrDefault(c => c.CourseId == id);
         context.Courses.Remove(courseToDelete);
         context.SaveChanges();
         return courseToDelete;
      });
   }
}
