using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Models.Notifications;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Command.CreateNotification
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, CreateNotificationDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;
        private readonly IUploader _uploader;

        public CreateNotificationCommandHandler(ISFDDBContext context, IMediator mediator, IUploader uploader)
        {
            _context = context;
            _mediator = mediator;
            _uploader = uploader;
        }

        public async Task<CreateNotificationDto> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var hasFile = request.Data.Attachment != null;
            var fileUrl = "";
            if (hasFile)
            {
                fileUrl = await _uploader.UploadFile(request.Data.Attachment.FileByte, 
                    "notification", 
                    Utils.GenerateFileCode("file", "/Uploads/notification/" + request.Data.Attachment.FileName) + "_" + request.Data.Attachment.FileName);
            }

            foreach (var oi in request.Data.OwnerId)
            {
                var ownerId = oi.ToString();
                var target = oi.ToString();
                if (request.Data.OwnerType.Equals(FCMMessage.TYPE_SINGLE))
                {
                    var userd = _context.Users
                        .Where(e => e.UserName.Equals(oi.ToString()))
                        .FirstOrDefault();
                    if (userd == null) continue;
                    ownerId = userd.Id.ToString(); 
                }

                var notification = new Domain.Entities.Notification
                {
                    OwnerType = request.Data.OwnerType,
                    OwnerId = ownerId,
                    Title = request.Data.Title,
                    Content = request.Data.Content,
                    IsDeletable = request.Data.IsDeletable,
                    CreateBy = request.AdminName,
                    Attachment = hasFile ? fileUrl : null
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync(cancellationToken);

                // send notification
                var notificationPayload = new FCMMessage()
                {
                    Title = notification.Title,
                    Body = notification.Content,
                    Type = "new_notification",
                    Key = "notification"
                };

                if (notification.OwnerType.Equals(FCMMessage.TYPE_TOPIC))
                {
                    notificationPayload.NotificationType = FCMMessage.TYPE_TOPIC;
                    notificationPayload.NotificationKey = target;
                }
                else
                {
                    notificationPayload.NotificationType = FCMMessage.TYPE_SINGLE;
                    
                    // get token from user
                    var user = await _context.Users
                        .Where(e => e.UserName.Equals(target.Trim()))
                        .FirstOrDefaultAsync();
                    
                    if (user != null)
                    {
                        notificationPayload.NotificationKey = user.FirebaseToken;
                    }
                }

                await _mediator.Publish(new SendNotification()
                {
                    Payload = notificationPayload
                }, cancellationToken);
            }

            return new CreateNotificationDto
            {
                Success = true,
                Message = "Notification has been successfully created"
            };
        }
    }
}