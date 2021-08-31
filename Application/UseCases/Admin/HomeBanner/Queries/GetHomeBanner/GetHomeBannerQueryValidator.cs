using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Queries.GetHomeBanner
{
    public class GetHomeBannerQueryValidator : AbstractValidator<GetHomeBannerQuery>
    {
        public GetHomeBannerQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
        }
    }
}
