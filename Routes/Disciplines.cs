using Microsoft.EntityFrameworkCore;
using UescCoursesAPI.Domain;
using UescCoursesAPI.Infrastructure.Persistence;

namespace UescCoursesAPI.API.Endpoints;

public static class Disciplines
{
   public static void RegisterDisciplinesEndpoints(this IEndpointRouteBuilder routes)
   {
      var disciplinesRoutes = routes.MapGroup("/disciplines/");
      
      disciplinesRoutes.MapGet("curriculums/{id}", (UescCourseAPIContext context, int id) => context.Disciplines.Include(d => d.Curriculums.Where(c => c.CurriculumId == id)).ToList());

      disciplinesRoutes.MapPost("", (int curriculumId, Discipline discipline, UescCourseAPIContext context) =>
      {
         var curriculum = context.Curriculums.Include(c => c.Disciplines).FirstOrDefault(c => c.CurriculumId == curriculumId);
         if (curriculum == null)
         {
            return Results.NotFound();
         }

         if (curriculum.Disciplines == null)
         {
            curriculum.Disciplines = new List<Discipline>();
         }
         curriculum.Disciplines.Add(discipline);
         context.SaveChanges();
         return Results.Ok(curriculum);
         // context.Disciplines.Add(discipline);
         // context.SaveChanges();
         // return discipline;
      });

      disciplinesRoutes.MapPost("curriculums/{curriculumId}", (Discipline disciplin, UescCourseAPIContext context, int curriculumId) =>
      {
         var curriculum = context.Curriculums.Include(c => c.Disciplines).FirstOrDefault(c => c.CurriculumId == curriculumId);
         if (curriculum == null)
         {
            return Results.NotFound();
         }

         if (curriculum.Disciplines == null)
         {
            curriculum.Disciplines = new List<Discipline>();
         }
         curriculum.Disciplines.Add(disciplin);
         context.SaveChanges();
         return Results.Ok(curriculum);
      });

   }
}
