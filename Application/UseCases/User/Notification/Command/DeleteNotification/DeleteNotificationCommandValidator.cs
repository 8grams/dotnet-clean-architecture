using FluentValidation;
using System.Linq;

namespace SFIDWebAPI.Application.UseCases.User.Notification.Command.DeleteNotification
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
