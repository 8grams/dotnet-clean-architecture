using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.UpdateGuideMaterial
{
    public class UpdateGuideMaterialCommandValidator : AbstractValidator<UpdateGuideMaterialCommand>
    {
        public UpdateGuideMaterialCommandValidator()
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