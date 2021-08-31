using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.AdditionalInfo.Queries.GetAdditionalInfoList
{
    public class AdditionalInfoListQueryValidator : AbstractValidator<AdditionalInfoListQuery>
    {
        public AdditionalInfoListQueryValidator()
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
