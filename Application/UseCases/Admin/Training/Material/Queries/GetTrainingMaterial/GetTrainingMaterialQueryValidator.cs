using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Queries.GetTrainingMaterial
{
    public class GetTrainingMaterialQueryValidator : AbstractValidator<GetTrainingMaterialQuery>
    {
        public GetTrainingMaterialQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
        }
    }
}
