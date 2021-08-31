using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Queries.GetHomeBanners
{
    public class GetHomeBannersQueryValidator : AbstractValidator<GetHomeBannersQuery>
    {
        public GetHomeBannersQueryValidator()
        {
            RuleFor(v => v.PagingPage)
                .GreaterThan(0);

            RuleFor(v => v.PagingLimit)
                .GreaterThan(0);

            RuleFor(v => v.SortColumn)
                .NotEmpty();

            RuleFor(v => v.SortType)
                .NotEmpty();
        }
    }
}
