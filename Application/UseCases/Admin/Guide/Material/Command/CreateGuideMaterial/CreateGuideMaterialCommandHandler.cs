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

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.CreateGuideMaterial
{
    public class CreateGuideMaterialCommandHandler : IRequestHandler<CreateGuideMaterialCommand, CreateGuideMaterialDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;
        private readonly IMediator _mediator;
        private readonly IMemoryData _memoryData;

        public CreateGuideMaterialCommandHandler(ISFDDBContext context, IMediator mediator, IUploader uploader, IMemoryData memoryData)
        {
            _context = context;
            _uploader = uploader;
            _mediator = mediator;
            _memoryData = memoryData;
        }

        public async Task<CreateGuideMaterialDto> Handle(CreateGuideMaterialCommand request, CancellationToken cancellationToken)
        {
            // validate file code
            var exists = await _context.GuideMaterials
                .Where(e => e.FileCode.Equals(request.Data.FileCode))
                .FirstOrDefaultAsync();
            
            if (exists != null) {
                return new CreateGuideMaterialDto
                {
                    Success = false,
                    Message = "File code already exists"
                };
            }

            var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "guide", request.Data.FileName);
            var material = new Domain.Entities.GuideMaterial
            {
                Title = request.Data.Title,
                MasterCarId = request.Data.CategoryId,
                ImageThumbnailId = request.Data.ImageThumbnailId,
                FileCode = request.Data.FileCode,
                FileType = Utils.GetFileExtension(request.Data.FileName),
                Link = fileUrl,
                PublishedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddYears(1),
                CreateBy = request.AdminName
            };

            _context.GuideMaterials.Add(material);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.Data.IsRecommended)
            {
                _context.Recommendations.Add(new Domain.Entities.Recommendation
                {
                    ContentId = material.Id,
                    ContentType = Domain.Entities.Recommendation.TYPE_GUIDE,
                    PublishedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddMonths(1)
                });
                await _context.SaveChangesAsync(cancellationToken);
            }

            // save new status
            _memoryData.NewGuide = material.Id;

            // save and publish notifications
            var notification = new Domain.Entities.Notification
            {
                OwnerType = "topic",
                OwnerId = "all-all-all",
                Title = "[New] Content Guide Material is Available",
                Content = material.Title,
                IsDeletable = true,
                CreateBy = request.AdminName
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync(cancellationToken);
            
            await _mediator.Publish(new SendNotification()
            {
                Payload = new FCMMessage{
                    Title = notification.Title,
                    Body = notification.Content,
                    Type = "guide",
                    Key = "new_material",
                    NotificationType = FCMMessage.TYPE_TOPIC,
                    NotificationKey = "new_material", // only new_material
                }
            }, cancellationToken);

            // update master car
            var masterCar = await _context.MasterCars.FindAsync(material.MasterCarId);
            masterCar.GuideLastUpdateDate = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateGuideMaterialDto
            {
                Success = true,
                Message = "Guide Material has been successfully created"
            };
        }
    }
}