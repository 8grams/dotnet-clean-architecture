using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.DeleteHomeBanner
{
    public class DeleteHomeBannerCommandValidator : AbstractValidator<DeleteHomeBannerCommand>
    {
        public DeleteHomeBannerCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.HasValue))
                .WithMessage("Id harus diisi");
        }
    }
}