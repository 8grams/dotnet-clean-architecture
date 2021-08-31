using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.UpdateTrainingMaterial
{
    public class UpdateTrainingMaterialCommandValidator : AbstractValidator<UpdateTrainingMaterialCommand>
    {
        public UpdateTrainingMaterialCommandValidator()
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