using FluentValidation;
using UescCoursesAPI.Domain;

namespace UescCoursesAPI.Validators;

public class UserValidator : AbstractValidator<User>
{
   public UserValidator()
   {
      RuleFor(u => u.Login)
         .NotEmpty().WithMessage("O campo Login é obrigatório")
         .EmailAddress().WithMessage("O campo Login deve ser um e-mail válido");
      
      RuleFor(u => u.Password)
         .NotEmpty().WithMessage("O campo Senha é obrigatório")
         .MinimumLength(6).WithMessage("O campo Senha deve ter no mínimo 6 caracteres");

      RuleFor(u => u.Rules)
         .NotEmpty().WithMessage("O campo Regras é obrigatório");
         
   }
}
