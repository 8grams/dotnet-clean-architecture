using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Queries.GetInfo
{
    public class GetInfoQueryValidator : AbstractValidator<GetInfoQuery>
    {
        public GetInfoQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
        }
    }
}
