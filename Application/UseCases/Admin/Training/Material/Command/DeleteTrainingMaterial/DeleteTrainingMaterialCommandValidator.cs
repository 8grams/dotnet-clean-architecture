using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.DeleteTrainingMaterial
{
    public class DeleteTrainingMaterialCommandValidator : AbstractValidator<DeleteTrainingMaterialCommand>
    {
        public DeleteTrainingMaterialCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.HasValue))
                .WithMessage("Id harus diisi");
        }
    }
}