using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Queries.GetMasterCar
{
    public class GetMasterCarQueryValidator : AbstractValidator<GetMasterCarQuery>
    {
        public GetMasterCarQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
        }
    }
}
