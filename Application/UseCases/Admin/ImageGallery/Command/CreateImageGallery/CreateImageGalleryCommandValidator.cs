using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.CreateImageGallery
{
    public class CreateImageGalleryCommandValidator : AbstractValidator<CreateImageGalleryCommand>
    {
        public CreateImageGalleryCommandValidator()
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty()
                .WithMessage("Judul harus diisi");

            RuleFor(x => x.Data.FileByte)
                .NotEmpty()
                .WithMessage("File harus disertakan");

            RuleFor(x => x.Data.Category)
                .NotEmpty()
                .WithMessage("Kategori harus diisi");
        }
    }
}