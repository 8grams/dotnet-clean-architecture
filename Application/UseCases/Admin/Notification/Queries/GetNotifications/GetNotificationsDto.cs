using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.Notification.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Queries.GetNotifications
{
    public class GetNotificationsDto : PaginationDto
    {
        public IList<NotificationData> Data { set; get; }
    }   
}
