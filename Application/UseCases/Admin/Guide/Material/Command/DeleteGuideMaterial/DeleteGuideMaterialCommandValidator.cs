using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.DeleteGuideMaterial
{
    public class DeleteGuideMaterialCommandValidator : AbstractValidator<DeleteGuideMaterialCommand>
    {
        public DeleteGuideMaterialCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.HasValue))
                .WithMessage("Id harus diisi");
        }
    }
}