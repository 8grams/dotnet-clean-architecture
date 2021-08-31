using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UpdateProfile
{
    public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator()
        {
            RuleFor(x => x.Data.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Data.Phone).NotEmpty();
        }
    }
}
