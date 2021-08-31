using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Auth.Command.Logout
{
    public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidator()
        {
            RuleFor(x => x.AuthToken)
                .NotEmpty()
                .WithMessage("Auth Token harus diisi");
        }
    }
}