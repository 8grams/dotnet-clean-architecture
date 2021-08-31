using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Models.Notifications;
using SFIDWebAPI.Infrastructure.Notifications.Email;
using Microsoft.Extensions.Options;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.Login
{
    public class SendPassword : INotification
    {
        public Domain.Entities.User User { set; get; }

        public class SendPasswordHandler : INotificationHandler<SendPassword>
        {
            private readonly IEmailService _emailService;
            private readonly EmailSettings _emailSettings;

            public SendPasswordHandler(IEmailService emailService, IOptions<EmailSettings> emailSettings)
            {
                _emailService = emailService;
                _emailSettings = emailSettings.Value;
            }

            public async Task Handle(SendPassword notification, CancellationToken cancellationToken)
            {
                var body = "Sistem mendeteksi adanya percobaan Login ke SFID Mobile Apps dengan meggunakan akun anda.\nDemi keamanan, anda disarankan untuk mengganti password.";
                var message = new EmailMessage()
                {
                    To = notification.User.Email,
                    From = _emailSettings.Sender,
                    FromName = _emailSettings.SenderName,
                    Subject = "Help up protect your account",
                    Body = body
                };

                await _emailService.SendAsync(message);
            }
        }
    }
}
