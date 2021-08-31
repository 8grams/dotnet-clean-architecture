using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Notification.Command.DeleteNotification
{
    public class DeleteNotificationCommand : BaseAdminQueryCommand, IRequest<DeleteNotificationDto>
    {
        public int UserId { set; get; }
        public IList<int?> Ids { set; get; }
    }
}