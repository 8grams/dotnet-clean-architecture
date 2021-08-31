using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.ChangeEmailPhone
{
    public class ChangeEmailPhoneCommandValidator : AbstractValidator<ChangeEmailPhoneCommand>
    {
        public ChangeEmailPhoneCommandValidator()
        {
            RuleFor(x => x.Data.Type).NotEmpty();
            RuleFor(x => x.Data.Identifier).NotEmpty();
        }
    }
}
