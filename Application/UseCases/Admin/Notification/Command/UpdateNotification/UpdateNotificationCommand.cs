using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Command.UpdateNotification
{
    public class UpdateNotificationCommand : BaseAdminQueryCommand, IRequest<UpdateNotificationDto>
    {
        public UpdateNotificationData Data { set; get; }
    }

    public class UpdateNotificationData
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public bool IsDeletable { set; get; }
        public string Attachment { set; get; }
    }
}
