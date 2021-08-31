using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Command.CreateNotification
{
    public class CreateNotificationCommand : BaseAdminQueryCommand, IRequest<CreateNotificationDto>
    {
        public CreateNotificationData Data { set; get; }
    }

    public class CreateNotificationData
    {
        public string OwnerType { set; get; }
        public IList<string> OwnerId { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public AttachmentData Attachment { set; get; }
        public bool IsDeletable { set; get; }
    }

    public class AttachmentData
    {
        public string FileName { set; get; }
        public string FileByte { set; get; }
    }
}
