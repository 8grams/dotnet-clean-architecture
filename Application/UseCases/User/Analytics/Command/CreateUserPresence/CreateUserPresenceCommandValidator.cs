using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Analytics.Command.CreateUserPresence
{
    public class CreateUserPresenceCommandValidator : AbstractValidator<CreateUserPresenceCommand>
    {
        public CreateUserPresenceCommandValidator()
        {
            RuleFor(v => v.Data.UserId)
                .NotEmpty();

            RuleFor(v => v.Data.Platform)
                .NotEmpty()
                .Must(platform =>
                {
                    return platform == "android" || platform == "ios";
                });
        }
    }
}
