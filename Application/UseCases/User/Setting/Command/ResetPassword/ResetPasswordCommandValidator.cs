using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Data.OldPassword).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Data.NewPassword).NotEmpty().MinimumLength(6);
        }
    }
}
