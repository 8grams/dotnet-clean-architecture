using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Command.DeleteInfo
{
    public class DeleteInfoCommandValidator : AbstractValidator<DeleteInfoCommand>
    {
        public DeleteInfoCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.HasValue))
                .WithMessage("Id harus diisi");
        }
    }
}