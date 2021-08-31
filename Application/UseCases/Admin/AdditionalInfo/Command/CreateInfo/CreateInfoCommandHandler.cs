using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;
using SFIDWebAPI.Application.Models.Notifications;
using SFIDWebAPI.Application.UseCases.Admin.Notification.Command.CreateNotification;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Command.CreateInfo
{
    public class CreateInfoCommandHandler : IRequestHandler<CreateInfoCommand, CreateInfoDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;
        private readonly IMediator _mediator;
        private readonly IMemoryData _memoryData;

        public CreateInfoCommandHandler(ISFDDBContext context, IMediator mediator, IUploader uploader, IMemoryData memoryData)
        {
            _context = context;
            _uploader = uploader;
            _mediator = mediator;
            _memoryData = memoryData;
        }

        public async Task<CreateInfoDto> Handle(CreateInfoCommand request, CancellationToken cancellationToken)
        {
            // validate file code
            var exists = await _context.AdditionalInfos
                .Where(e => e.FileCode.Equals(request.Data.FileCode))
                .FirstOrDefaultAsync();
            
            if (exists != null) {
                return new CreateInfoDto
                {
                    Success = false,
                    Message = "File code already exists"
                };
            }

            var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "info", request.Data.FileName);

            var info = new Domain.Entities.AdditionalInfo {
                Title = request.Data.Title,
                ImageThumbnailId = request.Data.ImageThumbnailId,
                PublishedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddYears(1),
                IsActive = request.Data.IsActive,
                IsDownloadable = request.Data.IsDownloadable,
                FileCode = request.Data.FileCode,
                FileType = Utils.GetFileExtension(request.Data.FileName),
                Link = fileUrl,
                CreateBy = request.AdminName
            };

            _context.AdditionalInfos.Add(info);

            await _context.SaveChangesAsync(cancellationToken);

            // save to recommendation
            if(request.Data.IsRecommended)
            {
                _context.Recommendations.Add(new Domain.Entities.Recommendation
                {
                    ContentId = info.Id,
                    ContentType = Domain.Entities.Recommendation.TYPE_INFO,
                    PublishedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddMonths(1),
                    CreateBy = request.AdminName
                });

                await _context.SaveChangesAsync(cancellationToken);
            }

            // save new status
            _memoryData.NewInfo = info.Id;

            // save notif and publish
            var notification = new Domain.Entities.Notification
            {
                OwnerType = "topic",
                OwnerId = "all-all-all",
                Title = "[New] Content Additional Info is Available",
                Content = info.Title,
                IsDeletable = true,
                CreateBy = request.AdminName
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync(cancellationToken);
            
            await _mediator.Publish(new SendNotification()
            {
                Payload = new FCMMessage {
                    Title = notification.Title,
                    Body = notification.Content,
                    Type = "info",
                    Key = "new_material",
                    NotificationType = FCMMessage.TYPE_TOPIC,
                    NotificationKey = "new_material", // only new_material
                }
            }, cancellationToken);

            return new CreateInfoDto
            {
                Success = true,
                Message = "Info has been successfully created"
            };
        }
    }
}