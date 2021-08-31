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

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Command.CreateBulletin
{
    public class CreateBulletinCommandHandler : IRequestHandler<CreateBulletinCommand, CreateBulletinDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;
        private readonly IMediator _mediator;
        private readonly IMemoryData _memoryData;

        public CreateBulletinCommandHandler(ISFDDBContext context, IMediator mediator, IUploader uploader, IMemoryData memoryData)
        {
            _context = context;
            _uploader = uploader;
            _mediator = mediator;
            _memoryData = memoryData;
        }

        public async Task<CreateBulletinDto> Handle(CreateBulletinCommand request, CancellationToken cancellationToken)
        {
            // validate file code
            var exists = await _context.Bulletins
                .Where(e => e.FileCode.Equals(request.Data.FileCode))
                .FirstOrDefaultAsync();
            
            if (exists != null) {
                return new CreateBulletinDto
                {
                    Success = false,
                    Message = "File code already exists"
                };
            }

            var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "bulletin", request.Data.FileName);
            var bulletin = new Domain.Entities.Bulletin {
                Title = request.Data.Title,
                ImageThumbnailId = request.Data.ImageThumbnailId,
                PublishedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddYears(1),
                IsActive = request.Data.IsActive,
                FileCode = request.Data.FileCode,
                FileType = Utils.GetFileExtension(request.Data.FileName),
                Link = fileUrl,
                CreateBy = request.AdminName
            };

            _context.Bulletins.Add(bulletin);

            await _context.SaveChangesAsync(cancellationToken);

            // save to recommendation
            if (request.Data.IsRecommended)
            {
                _context.Recommendations.Add(new Domain.Entities.Recommendation
                {
                    ContentId = bulletin.Id,
                    ContentType = Domain.Entities.Recommendation.TYPE_BULLETIN,
                    PublishedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddMonths(1),
                    CreateBy = request.AdminName
                });

                await _context.SaveChangesAsync(cancellationToken);
            }

            // save new status
            _memoryData.NewBulletin = bulletin.Id;
            
            // send notif
            var notification = new Domain.Entities.Notification
            {
                OwnerType = "topic",
                OwnerId = "all-all-all",
                Title = "[New] Content Bulletin is Available",
                Content = bulletin.Title,
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
                    Type = "bulletin",
                    Key = "new_material",
                    NotificationType = FCMMessage.TYPE_TOPIC,
                    NotificationKey = "new_material" // always new_material
                }
            }, cancellationToken);

            return new CreateBulletinDto
            {
                Success = true,
                Message = "Bulletin has been successfully created"
            };
        }
    }
}