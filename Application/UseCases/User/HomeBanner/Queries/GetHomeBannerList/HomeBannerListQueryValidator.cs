using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.HomeBanner.Queries.GetHomeBannerList
{
    public class HomeBannerListQueryValidator : AbstractValidator<HomeBannerListQuery>
    {
        public HomeBannerListQueryValidator()
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
