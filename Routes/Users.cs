using UescCoursesAPI.Domain;
using UescCoursesAPI.Infrastructure.Persistence;

namespace UescCoursesAPI.API.Endpoints;

public static class Users
{
   public static void RegisterUsersEndpoints(this IEndpointRouteBuilder routes)
   {
      var usersRoutes = routes.MapGroup("/users");

      usersRoutes.MapGet("", (UescCourseAPIContext context) => context.Users.ToList());

      usersRoutes.MapGet("/{id}", (int id, UescCourseAPIContext context) => context.Users.FirstOrDefault(u => u.UserId == id));
      usersRoutes.MapPost("", (User user, UescCourseAPIContext context) =>
      {
         context.Users.Add(user);
         context.SaveChanges();
         return user;
      });
      usersRoutes.MapPut("/{id}", (int id, User user, UescCourseAPIContext context) =>
      {
         var userToUpdate = context.Users.FirstOrDefault(u => u.UserId == id);

         if (userToUpdate == null)
         {
            return Results.NotFound();
         }

         userToUpdate.Login = user.Login;
         userToUpdate.Password = user.Password;
         userToUpdate.Rules = user.Rules;
         context.SaveChanges();
         return Results.Ok(userToUpdate);
      });

      usersRoutes.MapDelete("/{id}", (int id, UescCourseAPIContext context) =>
      {
         var userToDelete = context.Users.FirstOrDefault(u => u.UserId == id);

         if (userToDelete == null)
         {
            return Results.NotFound();
         }

         context.Users.Remove(userToDelete);
         context.SaveChanges();
         return Results.Ok(userToDelete);
      });
   }
}
