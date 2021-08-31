using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Models.Notifications;
using SFIDWebAPI.Infrastructure.Notifications.Email;
using Microsoft.Extensions.Options;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.ForgotPassword
{
    public class AfterForgotPassword : INotification
    {
        public Domain.Entities.User User { set; get; }

        public class AfterForgotPasswordHandler : INotificationHandler<AfterForgotPassword>
        {
            private readonly IEmailService _emailService;
            private readonly EmailSettings _emailSettings;

            public AfterForgotPasswordHandler(IEmailService emailService, IOptions<EmailSettings> emailSettings)
            {
                _emailService = emailService;
                _emailSettings = emailSettings.Value;
            }

            public async Task Handle(AfterForgotPassword notification, CancellationToken cancellationToken)
            {
                var message = new EmailMessage()
                {
                    To = notification.User.Email,
                    From = _emailSettings.Sender,
                    FromName = _emailSettings.SenderName,
                    Subject = "New Password",
                    Body = $"Password SFID anda berhasil di reset. Silakan gunakan kode ini untuk Login: \n{notification.User.RawPassword} \nAbaikan pesan ini jika Anda tidak sedang melakukan aktifitas SFID Mobile Apps."
                };

                await _emailService.SendAsync(message);
            }
        }
    }
}
