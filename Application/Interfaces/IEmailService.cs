using System.Threading.Tasks;
using WebApi.Application.Models.Notifications;

namespace WebApi.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage message);
    }
}
