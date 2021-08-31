using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Queries.GetBulletin
{
    public class GetBulletinQueryValidator : AbstractValidator<GetBulletinQuery>
    {
        public GetBulletinQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
        }
    }
}
