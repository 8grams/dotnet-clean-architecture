using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Command.DeleteNotification
{
    public class DeleteNotificationCommand : BaseAdminQueryCommand, IRequest<DeleteNotificationDto>
    {
        public IList<int?> Ids { set; get; }
    }
}