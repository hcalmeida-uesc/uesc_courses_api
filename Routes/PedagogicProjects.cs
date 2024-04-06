using UescCoursesAPI.Domain;
using UescCoursesAPI.Infrastructure.Persistence;

namespace UescCoursesAPI.API.Endpoints;

public static class PedagogicProjects
{
   public static void RegisterPedagogicProjectsEndpoints(this IEndpointRouteBuilder routes)
   {
      var pedagogicProjectsRoutes = routes.MapGroup("/pedagogicProjects");

      pedagogicProjectsRoutes.MapGet("/courses/{id}/pedagogicProjects", (int id, UescCourseAPIContext context) => context.PedagogicProjects.Where(p => p.CourseId == id).ToList());

      pedagogicProjectsRoutes.MapPost("/courses/{id}/pedagogicProjects", (int id, PedagogicProject proj, UescCourseAPIContext context) =>
      {
         proj.CourseId = id;
         context.PedagogicProjects.Add(proj);
         context.SaveChanges();
         return proj;
      });
   }
}
