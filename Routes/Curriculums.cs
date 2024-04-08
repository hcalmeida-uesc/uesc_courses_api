using UescCoursesAPI.Domain;
using UescCoursesAPI.Infrastructure.Persistence;

namespace UescCoursesAPI.API.Endpoints;

public static class Curriculums
{
   public static void RegisterCurriculumssEndpoints(this IEndpointRouteBuilder routes)
   {
      var curriculumsRoutes = routes.MapGroup("/curriculums");
      
      curriculumsRoutes.MapGet("", (UescCourseAPIContext context) => context.Curriculums.ToList());

      curriculumsRoutes.MapPost("", (Curriculum curriculum, UescCourseAPIContext context) =>
      {
         context.Curriculums.Add(curriculum);
         context.SaveChanges();
         return curriculum;
      });
   }
}
