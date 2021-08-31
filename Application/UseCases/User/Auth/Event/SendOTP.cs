using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;
using SFIDWebAPI.Application.Models.Notifications;
using SFIDWebAPI.Infrastructure.Notifications.Email;
using SFIDWebAPI.Infrastructure.Persistences;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Event
{
    public class SendOTP : INotification
    {
        public Domain.Entities.User User { set; get; }

        public class SendOTPHandler : INotificationHandler<SendOTP>
        {
            private readonly ISMSService _smsService;
            private readonly IEmailService _emailService;
            private readonly EmailSettings _emailSettings;
            private readonly ISFDDBContext _context;

            public SendOTPHandler(ISFDDBContext context, ISMSService smsService, IEmailService emailService, IOptions<EmailSettings> emailSettings)
            {
                _smsService = smsService;
                _context = context;
                _emailService = emailService;
                _emailSettings = emailSettings.Value;
            }

            public async Task Handle(SendOTP notification, CancellationToken cancellationToken)
            {
                // create OTP
                var pin = DBUtil.GeneratePin();
                var otp = new Domain.Entities.OTP()
                {
                    Pin = pin,
                    UserId = notification.User.Id,
                    ExpiresAt = DateTime.Now.AddMinutes(5)
                };

                _context.OTPs.Add(otp);
                await _context.SaveChangesAsync(cancellationToken);

                var body = $"Kode OTP SFID anda adalah {pin} . OTP ini bersifat RAHASIA.\n Abaikan pesan ini jika Anda tidak melakukan aktifitas di SFID Mobile Apps.";
                if (!Utils.IsDevelopment())
                {
                    // send SMS
                    await _smsService.SendAsync(new SMSMessage()
                    {
                        BodyMessage = body,
                        DestinationNo = notification.User.Phone
                    });
                }

                // send email
                var message = new EmailMessage()
                {
                    To = notification.User.Email,
                    From = _emailSettings.Sender,
                    FromName = _emailSettings.SenderName,
                    Subject = "OTP of Mitsubishi Sales Force ID",
                    Body = body
                };
                await _emailService.SendAsync(message);
            }
        }
    }
}
