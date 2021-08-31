using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.CreateHomeBanner
{
    public class CreateHomeBannerCommandValidator : AbstractValidator<CreateHomeBannerCommand>
    {
        public CreateHomeBannerCommandValidator()
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