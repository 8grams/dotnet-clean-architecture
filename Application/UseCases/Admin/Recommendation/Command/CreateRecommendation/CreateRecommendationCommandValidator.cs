using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Command.CreateRecommendation
{
    public class CreateRecommendationCommandValidator : AbstractValidator<CreateRecommendationCommand>
    {
        public CreateRecommendationCommandValidator()
        {
            RuleFor(x => x.Data.ContentId)
                .GreaterThan(0)
                .WithMessage("ID harus diisi");

            RuleFor(x => x.Data.ContentType).NotEmpty().Must(type =>
            {
                return type == "bulletin" || type == "info" || type == "guide" || type == "training";
            }).WithMessage("Type harus diisi");
        }
    }
}