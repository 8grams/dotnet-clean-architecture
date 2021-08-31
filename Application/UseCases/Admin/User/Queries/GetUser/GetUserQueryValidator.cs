using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Queries.GetUser
{
    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
        }
    }
}
