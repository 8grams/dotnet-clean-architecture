using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Material.Queries.GetGuideMaterialList
{
    public class GuideMaterialListQueryValidator : AbstractValidator<GuideMaterialListQuery>
    {
        public GuideMaterialListQueryValidator()
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
