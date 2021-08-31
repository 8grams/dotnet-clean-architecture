using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Auth.Command.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Data.Email)
                .NotEmpty()
                .WithMessage("Email harus diisi");
            RuleFor(x => x.Data.Password)
                .NotEmpty()
                .WithMessage("Password harus diisi");
        }
    }
}