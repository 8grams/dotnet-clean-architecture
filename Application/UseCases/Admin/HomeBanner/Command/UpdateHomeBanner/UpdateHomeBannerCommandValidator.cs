using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.UpdateHomeBanner
{
    public class UpdateHomeBannerCommandValidator : AbstractValidator<UpdateHomeBannerCommand>
    {
        public UpdateHomeBannerCommandValidator()
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty()
                .WithMessage("Nama harus diisi");
            
            RuleFor(x => x.Data.ImageId)
                .NotEmpty()
                .WithMessage("Gambar harus disertakan");
        }
    }
}