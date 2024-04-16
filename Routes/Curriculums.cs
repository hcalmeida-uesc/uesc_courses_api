using Microsoft.EntityFrameworkCore;
using UescCoursesAPI.Domain;
using UescCoursesAPI.Infrastructure.Persistence;

namespace UescCoursesAPI.API.Endpoints;

public static class Curriculums
{
   public static void RegisterCurriculumsEndpoints(this IEndpointRouteBuilder routes)
   {
      var curriculumsRoutes = routes.MapGroup("/curriculums/");
      
      curriculumsRoutes.MapGet("pedagogicProjects/{id}", (UescCourseAPIContext context, int id) => context.Curriculums.Include(c => c.Disciplines).Where(c => c.PedagogicProjectId == id).ToList());

      curriculumsRoutes.MapPost("", (Curriculum curriculum, UescCourseAPIContext context) =>
      {
         context.Curriculums.Add(curriculum);
         context.SaveChanges();
         return curriculum;
      });

   }
}
