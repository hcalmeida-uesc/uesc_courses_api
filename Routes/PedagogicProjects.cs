using UescCoursesAPI.Domain;
using UescCoursesAPI.Infrastructure.Persistence;

namespace UescCoursesAPI.API.Endpoints;

public static class PedagogicProjects
{
   public static void RegisterPedagogicProjectsEndpoints(this IEndpointRouteBuilder routes)
   {
      var pedagogicProjectsRoutes = routes.MapGroup("/courses/{id}/pedagogicProjects");

      pedagogicProjectsRoutes.MapGet("", (int id, UescCourseAPIContext context) => context.PedagogicProjects.Where(p => p.CourseId == id).ToList());

      pedagogicProjectsRoutes.MapPost("", (int id, PedagogicProject proj, UescCourseAPIContext context) =>
      {
         proj.CourseId = id;
         context.PedagogicProjects.Add(proj);
         context.SaveChanges();
         return proj;
      });
   }
}
