using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Queries.GetFAQs
{
    public class GetFAQsQueryValidator : AbstractValidator<GetFAQsQuery>
    {
        public GetFAQsQueryValidator()
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
