using System.Linq;
using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Command.DeleteNotification
{
    public class DeleteNotificationCommandValidator : AbstractValidator<DeleteNotificationCommand>
    {
        public DeleteNotificationCommandValidator()
        {
            RuleFor(x => x.Ids)
                .Must(e => e.All(item => item.HasValue))
                .WithMessage("Id harus diisi");
        }
    }
}