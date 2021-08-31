using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Category.Queries.GetGuideCategoryDetail
{
    public class GuideCategoryDetailQueryValidator : AbstractValidator<GuideCategoryDetailQuery>
    {
        public GuideCategoryDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0);
        }
    }
}
