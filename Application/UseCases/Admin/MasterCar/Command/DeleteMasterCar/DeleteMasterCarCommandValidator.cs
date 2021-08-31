using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Command.DeleteMasterCar
{
    public class DeleteMasterCarCommandValidator : AbstractValidator<DeleteMasterCarCommand>
    {
        public DeleteMasterCarCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.HasValue))
                .WithMessage("Id harus diisi");
        }
    }
}