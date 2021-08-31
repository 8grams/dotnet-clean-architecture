using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.PKT.History.Queries.GetHistoryList
{
    public class PKTHistoryListQueryValidator : AbstractValidator<PKTHistoryListQuery>
    {
        public PKTHistoryListQueryValidator()
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
