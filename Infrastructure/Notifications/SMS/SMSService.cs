using System.Threading.Tasks;
using System.Net.Http;
using WebApi.Application.Interfaces;
using WebApi.Application.Models.Notifications;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace WebApi.Infrastructure.Notifications.SMS
{
    public class SMSService : ISMSService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly SMSSetting _smsSetting;

        public SMSService(IHttpClientFactory clientFactory, IOptions<SMSSetting> smsSetting)
        {
            _clientFactory = clientFactory;
            _smsSetting = smsSetting.Value;
        }

        public async Task SendAsync(SMSMessage message)
        {
            var content = new StringContent($"ClientID={_smsSetting.ClientId}" +
                $"&UserName={_smsSetting.Username}" +
                $"&Password={_smsSetting.Password}" +
                $"&BodyMessage={message.BodyMessage}" +
                $"&DestinationNo={message.DestinationNo}");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var client = _clientFactory.CreateClient();
            await client.PostAsync(_smsSetting.Host, content);
            client.Dispose();
        }
    }
}
