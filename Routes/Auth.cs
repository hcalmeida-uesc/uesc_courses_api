using Microsoft.AspNetCore.Mvc;
using UescCoursesAPI.API.ViewModels;
using UescCoursesAPI.Application.Services;
using UescCoursesAPI.Services.DTO;

namespace UescCoursesAPI.API.Endpoints;


public static class Auth
{
   public static void RegisterAuthEndpoints(this IEndpointRouteBuilder routes)
   {
      var authRoutes = routes.MapGroup("/auth");
      authRoutes.MapPost("/login", (
         [FromBody] UserPostDTO authuser,
         ILoginService login)  =>
         {
            var result = login.Authenticate(authuser);

            if (result is not null)
               return Results.Ok(result);
            else
               return Results.BadRequest("Usuário ou senha inválidos");
         });
   }
}
