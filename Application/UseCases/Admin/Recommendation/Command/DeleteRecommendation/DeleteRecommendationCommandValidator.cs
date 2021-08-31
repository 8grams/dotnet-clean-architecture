using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Command.DeleteRecommendation
{
    public class DeleteRecommendationCommandValidator : AbstractValidator<DeleteRecommendationCommand>
    {
        public DeleteRecommendationCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.ContentId > 0 && !string.IsNullOrEmpty(item.ContentType)))
                .WithMessage("Id harus diisi");
        }
    }
}