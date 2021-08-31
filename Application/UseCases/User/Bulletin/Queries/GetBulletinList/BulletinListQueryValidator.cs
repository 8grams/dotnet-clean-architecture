using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Bulletin.Queries.GetBulletinList
{
    public class BulletinListQueryValidator : AbstractValidator<BulletinListQuery>
    {
        public BulletinListQueryValidator()
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
