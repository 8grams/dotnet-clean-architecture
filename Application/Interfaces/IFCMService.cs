using System.Threading.Tasks;
using SFIDWebAPI.Application.Models.Notifications;

namespace SFIDWebAPI.Application.Interfaces
{
    public interface IFCMService
    {
        Task SendAsync(FCMMessage message);
    }
}
