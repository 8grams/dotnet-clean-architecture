using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Command.UpdateMasterCar
{
    public class UpdateMasterCarCommandValidator : AbstractValidator<UpdateMasterCarCommand>
    {
        public UpdateMasterCarCommandValidator()
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty()
                .WithMessage("Nama harus diisi");

            RuleFor(x => x.Data.Tag)
                .NotEmpty()
                .WithMessage("Tag harus diisi");

            RuleFor(x => x.Data.ImageCoverId)
                .NotEmpty()
                .WithMessage("Gambar Cover harus disertakan");

            RuleFor(x => x.Data.ImageThumbnailId)
                .NotEmpty()
                .WithMessage("Gambar Thumbnail harus disertakan");

            RuleFor(x => x.Data.ImageLogoId)
                .NotEmpty()
                .WithMessage("Gambar Logo harus disertakan");
        }
    }
}