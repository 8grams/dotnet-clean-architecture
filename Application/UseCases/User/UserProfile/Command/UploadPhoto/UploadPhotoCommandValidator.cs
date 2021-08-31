using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UploadPhoto
{
    public class UploadPhotoCommandValidator : AbstractValidator<UploadPhotoCommand>
    {
        public UploadPhotoCommandValidator()
        {
            RuleFor(x => x.Data.FileByte).NotEmpty();
        }
    }
}
