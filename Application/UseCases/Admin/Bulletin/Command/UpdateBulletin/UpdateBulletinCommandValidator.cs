using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Command.UpdateBulletin
{
    public class UpdateBulletinCommandValidator : AbstractValidator<UpdateBulletinCommand>
    {
        public UpdateBulletinCommandValidator()
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