using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Training.Category.Queries.GetTrainingCategoryDetail
{
    public class TrainingCategoryDetailQueryValidator : AbstractValidator<TrainingCategoryDetailQuery>
    {
        public TrainingCategoryDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0);
        }
    }
}
