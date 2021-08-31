using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Command.CreateNotification
{
    public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidator()
        {
            RuleFor(x => x.Data.OwnerId)
                .NotEmpty()
                .WithMessage("Owner ID harus diisi");

            RuleFor(x => x.Data.OwnerType)
                .NotEmpty()
                .Must(type =>
                {
                    return type == "topic" || type == "single";
                })
                .WithMessage("Owner Type harus diisi");

            RuleFor(x => x.Data.Title)
                .NotEmpty()
                .WithMessage("Title harus diisi");

            RuleFor(x => x.Data.Content)
                .NotEmpty()
                .WithMessage("Pesan Type harus diisi");
        }
    }
}