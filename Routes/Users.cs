using FluentValidation;
using FluentValidation.Results;
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
      usersRoutes.MapPost("", async (IValidator<User> validator, User user, UescCourseAPIContext context) =>
      {
         ValidationResult validationResult = await validator.ValidateAsync(user);

         if (!validationResult.IsValid)
         {
            return Results.ValidationProblem(validationResult.ToDictionary());
         }

         context.Users.Add(user);
         context.SaveChanges();
         return Results.Created($"/{user.UserId}",user);
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
