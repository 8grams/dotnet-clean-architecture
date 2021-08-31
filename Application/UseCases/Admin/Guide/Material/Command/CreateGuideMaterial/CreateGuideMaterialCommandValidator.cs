using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.CreateGuideMaterial
{
    public class CreateGuideMaterialCommandValidator : AbstractValidator<CreateGuideMaterialCommand>
    {
        public CreateGuideMaterialCommandValidator()
        {
            RuleFor(x => x.Data.Title)
                .NotEmpty()
                .WithMessage("Judul harus diisi");

            RuleFor(x => x.Data.FileCode)
                .NotEmpty()
                .WithMessage("File Code harus disertakan");

            RuleFor(x => x.Data.FileByte)
                .NotEmpty()
                .WithMessage("File harus disertakan");

            RuleFor(x => x.Data.FileName)
                .NotEmpty()
                .WithMessage("File Name harus disertakan");

            RuleFor(x => x.Data.ImageThumbnailId)
                .NotEmpty()
                .WithMessage("Gambar Thumbnail harus disertakan");
        }
    }
}