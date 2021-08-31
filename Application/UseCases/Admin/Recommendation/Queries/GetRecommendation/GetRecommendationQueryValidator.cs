using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Queries.GetRecommendation
{
    public class GetRecommendationQueryValidator : AbstractValidator<GetRecommendationQuery>
    {
        public GetRecommendationQueryValidator()
        {
            RuleFor(v => v.ContentId).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
            RuleFor(v => v.ContentType).NotEmpty().WithMessage("Type harus terdaftar");
        }
    }
}
