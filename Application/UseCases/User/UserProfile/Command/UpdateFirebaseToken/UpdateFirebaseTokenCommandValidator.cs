using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UpdateFirebaseToken
{
    public class UpdateFirebaseTokenCommandValidator : AbstractValidator<UpdateFirebaseTokenCommand>
    {
        public UpdateFirebaseTokenCommandValidator()
        {
            RuleFor(x => x.Data.Token).NotEmpty();
        }
    }
}
