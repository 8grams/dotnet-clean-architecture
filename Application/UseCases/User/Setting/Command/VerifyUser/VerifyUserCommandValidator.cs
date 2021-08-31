using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.VerifyUser
{
    public class VerifyUserCommandValidator : AbstractValidator<VerifyUserCommand>
    {
        public VerifyUserCommandValidator()
        {
            RuleFor(x => x.Data.Password).NotEmpty().MinimumLength(6);
        }
    }
}
