using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Queries.GetGuideMaterial
{
    public class GetGuideMaterialQueryValidator : AbstractValidator<GetGuideMaterialQuery>
    {
        public GetGuideMaterialQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
        }
    }
}
