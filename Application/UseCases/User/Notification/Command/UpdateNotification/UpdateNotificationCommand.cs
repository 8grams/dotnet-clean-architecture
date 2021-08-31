using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Notification.Command.UpdateNotification
{
    public class UpdateNotificationCommand : BaseQueryCommand, IRequest<UpdateNotificationDto>
    {
        public List<NotificationUpdateData> Data { set; get; } 
    }

    public class NotificationUpdateData
    {
        public int Id { set; get; }
        public string Status { set; get; }
    }
}
