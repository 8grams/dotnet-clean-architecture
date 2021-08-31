using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Data.SalesCode)
                .NotEmpty()
                .WithMessage("Sales Code harus diisi");
            RuleFor(x => x.Data.Password)
                .NotEmpty()
                .WithMessage("Password harus diisi");
            RuleFor(x => x.Data.DeviceId)
                .NotEmpty()
                .WithMessage("Password harus diisi");
        }
    }
}
