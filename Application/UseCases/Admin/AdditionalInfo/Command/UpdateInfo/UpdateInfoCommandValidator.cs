using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Command.UpdateInfo
{
    public class UpdateInfoCommandValidator : AbstractValidator<UpdateInfoCommand>
    {
        public UpdateInfoCommandValidator()
        {
            RuleFor(x => x.Data.Title)
                .NotEmpty()
                .WithMessage("Judul harus diisi");

            RuleFor(x => x.Data.ImageThumbnailId)
                .NotEmpty()
                .WithMessage("Gambar Thumbnail harus disertakan");
        }
    }
}