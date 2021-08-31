using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Bulletin.Queries.GetBulletinDetail
{
    public class BulletinDetailQueryValidator : AbstractValidator<BulletinDetailQuery>
    {
        public BulletinDetailQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
        }
    }
}
