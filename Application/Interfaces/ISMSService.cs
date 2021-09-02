using System.Threading.Tasks;
using WebApi.Application.Models.Notifications;

namespace WebApi.Application.Interfaces
{
    public interface ISMSService
    {
        Task SendAsync(SMSMessage message);
    }
}
