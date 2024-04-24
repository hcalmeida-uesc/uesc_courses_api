using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using UescCoursesAPI.Domain;
using UescCoursesAPI.Services.DTO;
using UescCoursesAPI.Infrastructure.Persistence;

namespace UescCoursesAPI.API.Endpoints;

public static class Users
{
   public static void RegisterUsersEndpoints(this IEndpointRouteBuilder routes)
   {
      var usersRoutes = routes.MapGroup("/users");

      usersRoutes.MapGet("", (UescCourseAPIContext context) => context.Users.ToList());

      usersRoutes.MapGet("/{id}", (int id, UescCourseAPIContext context) => context.Users.FirstOrDefault(u => u.UserId == id));
      
      usersRoutes.MapPost("", async (
         IValidator<UserPostDTO> validator,
         [FromBody] UserPostDTO userPost,
         UescCourseAPIContext context) =>
         {
            ValidationResult validationResult = await validator.ValidateAsync(userPost);

            if (!validationResult.IsValid)
            {
               return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var user = new User(userPost.Login, userPost.Password, userPost.Rules);

            context.Users.Add(user);
            context.SaveChanges();
            return Results.Created($"/{user.UserId}",user);
      });
      usersRoutes.MapPut("/{id}", (
         int id,
         [FromBody] UserPostDTO user,
         UescCourseAPIContext context) =>
         {
            var userToUpdate = context.Users.FirstOrDefault(u => u.UserId == id);

            if (userToUpdate == null)
            {
               return Results.NotFound();
            }

            userToUpdate.Update(user.Login, user.Password, user.Rules);
            context.SaveChanges();
            return Results.Ok(userToUpdate);
      });

      // TODO: usersRoutes.MapPatch("/{id}", ...

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
