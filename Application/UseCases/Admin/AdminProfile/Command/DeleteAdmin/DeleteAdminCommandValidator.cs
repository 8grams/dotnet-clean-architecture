using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.DeleteAdmin
{
    public class DeleteAdminCommandValidator : AbstractValidator<DeleteAdminCommand>
    {
        public DeleteAdminCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.HasValue))
                .WithMessage("Id harus diisi");
        }
    }
}