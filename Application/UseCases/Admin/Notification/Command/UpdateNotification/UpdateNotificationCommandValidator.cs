using FluentValidation;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Command.UpdateNotification
{
    public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
    {
        public UpdateNotificationCommandValidator()
        {
            RuleFor(x => x.Data.Title)
                .NotEmpty()
                .WithMessage("Judul harus diisi");

            RuleFor(x => x.Data.Content)
                .NotEmpty()
                .WithMessage("Pesan harus diisi");
        }
    }
}