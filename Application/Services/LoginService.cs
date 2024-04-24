using UescCoursesAPI.Domain;
using UescCoursesAPI.Infrastructure.Persistence;
using UescCoursesAPI.Services.DTO;

namespace UescCoursesAPI.Application.Services;

public interface ILoginService
{
   UserAuthDTO? Authenticate(UserPostDTO authUser);
}
public class LoginService : ILoginService
{
   private readonly IAuthManager _authManager;
   private readonly UescCourseAPIContext _context;
   public LoginService(IAuthManager authManager, UescCourseAPIContext context)
   {
      _authManager = authManager;
      _context = context;
   }
   public UserAuthDTO? Authenticate(UserPostDTO authUser)
   {
      var _passHashed = Utils.ComputeSha256Hash(authUser.Password);

      var user = _context.Users.FirstOrDefault(u => u.Login == authUser.Login && u.Password == _passHashed);

      if (user is null)
         return null;

      var _token = _authManager.GenerateJwtToken(user.Login, user.Rules.ToString());
      
      return new UserAuthDTO(user.Login, _token);

   }
}
