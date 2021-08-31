using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.User.Notification.Queries.GetNotificationList
{
    public class NotificationListQueryValidator : AbstractValidator<NotificationListQuery>
    {
        public NotificationListQueryValidator()
        {
            RuleFor(v => v.PagingPage)
                .GreaterThan(0);

            RuleFor(v => v.PagingLimit)
                .GreaterThan(0);

            RuleFor(v => v.SortColumn)
                .NotEmpty();

            RuleFor(v => v.SortType)
                .NotEmpty();
        }
    }
}
