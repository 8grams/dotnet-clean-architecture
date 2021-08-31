using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Queries.GetBulletins
{
    public class GetBulletinsQueryValidator : AbstractValidator<GetBulletinsQuery>
    {
        public GetBulletinsQueryValidator()
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
