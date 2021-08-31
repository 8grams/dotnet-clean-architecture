using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Training.Category.Queries.GetTrainingCategoryList
{
    public class TrainingCategoryListQueryValidator : AbstractValidator<TrainingCategoryListQuery>
    {
        public TrainingCategoryListQueryValidator()
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
