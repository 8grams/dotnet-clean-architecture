using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Queries.GetFAQ
{
    public class GetFAQQueryValidator : AbstractValidator<GetFAQQuery>
    {
        public GetFAQQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
        }
    }
}
