using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Queries.GetNotification
{
    public class GetNotificationQueryValidator : AbstractValidator<GetNotificationQuery>
    {
        public GetNotificationQueryValidator()
        {
            RuleFor(v => v.Id).NotEmpty().GreaterThan(0).WithMessage("ID harus terdaftar");
        }
    }
}
