using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Queries.GetInfos
{
    public class GetInfosQueryValidator : AbstractValidator<GetInfosQuery>
    {
        public GetInfosQueryValidator()
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
