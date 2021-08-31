using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.DeleteFAQ
{
    public class DeleteFAQCommandValidator : AbstractValidator<DeleteFAQCommand>
    {
        public DeleteFAQCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.HasValue))
                .WithMessage("Id harus diisi");
        }
    }
}