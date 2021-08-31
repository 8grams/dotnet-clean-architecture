using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.Notification.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Queries.GetNotification
{
    public class GetNotificationDto : BaseDto
    {
        public NotificationData Data { set; get; }
    }   
}
