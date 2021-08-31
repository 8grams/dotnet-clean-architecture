using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.CreateTrainingMaterial
{
    public class CreateTrainingMaterialCommandValidator : AbstractValidator<CreateTrainingMaterialCommand>
    {
        public CreateTrainingMaterialCommandValidator()
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