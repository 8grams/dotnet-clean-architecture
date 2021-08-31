using System.Threading.Tasks;
using SFIDWebAPI.Application.Models.Notifications;

namespace SFIDWebAPI.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage message);
    }
}
