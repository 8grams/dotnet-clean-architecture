using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System;
using MailKit.Net.Smtp;
using MimeKit;
using WebApi.Application.Interfaces;
using WebApi.Application.Models.Notifications;
using Microsoft.Extensions.Options;

namespace WebApi.Infrastructure.Notifications.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHostEnvironment _env;

        public EmailService(IOptions<EmailSettings> emailSettings, IHostEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }

        public async Task SendAsync(EmailMessage msg)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(msg.FromName, msg.From));
                mimeMessage.To.Add(new MailboxAddress(msg.To, msg.To));
                mimeMessage.Subject = msg.Subject;
                mimeMessage.Body = new TextPart("html")
                {
                    Text = msg.Body
                };

                using var client = new SmtpClient
                {
                    ServerCertificateValidationCallback = (s, c, h, e) => true
                };

                await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);

                // Note: only needed if the SMTP server requires authentication
                await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);

                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
