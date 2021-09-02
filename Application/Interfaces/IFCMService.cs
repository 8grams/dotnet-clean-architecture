using System.Threading.Tasks;
using WebApi.Application.Models.Notifications;

namespace WebApi.Application.Interfaces
{
    public interface IFCMService
    {
        Task SendAsync(FCMMessage message);
    }
}
