using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Command.DeleteBulletin
{
    public class DeleteBulletinCommandValidator : AbstractValidator<DeleteBulletinCommand>
    {
        public DeleteBulletinCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.HasValue))
                .WithMessage("Id harus diisi");
        }
    }
}