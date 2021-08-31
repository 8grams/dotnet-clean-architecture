using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Command.DeletePositionMeta
{
    public class DeletePositionMetaCommandValidator : AbstractValidator<DeletePositionMetaCommand>
    {
        public DeletePositionMetaCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.HasValue))
                .WithMessage("Id harus diisi");
        }
    }
}