using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Category.Queries.GetGuideCategoryList
{
    public class GuideCategoryListQueryValidator : AbstractValidator<GuideCategoryListQuery>
    {
        public GuideCategoryListQueryValidator()
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
