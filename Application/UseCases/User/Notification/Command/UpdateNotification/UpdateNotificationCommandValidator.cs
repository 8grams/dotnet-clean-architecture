using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Notification.Command.UpdateNotification
{
    public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
    {
        public UpdateNotificationCommandValidator()
        {
            RuleFor(x => x.Data).Must(data => data.Count > 0);
            RuleForEach(x => x.Data).SetValidator(new NotificationItemValidator());
        }
    }

    public class NotificationItemValidator : AbstractValidator<NotificationUpdateData>
    {
        public NotificationItemValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Status).NotEmpty().Must(type =>
            {
                return type == "read" || type == "unread";
            });
        }
    }
}
