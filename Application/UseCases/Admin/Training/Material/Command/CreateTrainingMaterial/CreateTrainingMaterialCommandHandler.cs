using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;
using SFIDWebAPI.Application.Models.Notifications;
using SFIDWebAPI.Application.UseCases.Admin.Notification.Command.CreateNotification;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.CreateTrainingMaterial
{
    public class CreateTrainingMaterialCommandHandler : IRequestHandler<CreateTrainingMaterialCommand, CreateTrainingMaterialDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;
        private readonly IMediator _mediator;
        private readonly IMemoryData _memoryData;

        public CreateTrainingMaterialCommandHandler(ISFDDBContext context, IMediator mediator, IUploader uploader, IMemoryData memoryData)
        {
            _context = context;
            _uploader = uploader;
            _mediator = mediator;
            _memoryData = memoryData;
        }

        public async Task<CreateTrainingMaterialDto> Handle(CreateTrainingMaterialCommand request, CancellationToken cancellationToken)
        {
            // validate file code
            var exists = await _context.TrainingMaterials
                .Where(e => e.FileCode.Equals(request.Data.FileCode))
                .FirstOrDefaultAsync();
            
            if (exists != null) {
                return new CreateTrainingMaterialDto
                {
                    Success = false,
                    Message = "File code already exists"
                };
            }

            var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "training", request.Data.FileName);

            var material = new Domain.Entities.TrainingMaterial
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

            _context.TrainingMaterials.Add(material);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.Data.IsRecommended)
            {
                _context.Recommendations.Add(new Domain.Entities.Recommendation
                {
                    ContentId = material.Id,
                    ContentType = Domain.Entities.Recommendation.TYPE_TRAINING,
                    PublishedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddMonths(1)
                });
                await _context.SaveChangesAsync(cancellationToken);
            }

            // save new status
            _memoryData.NewTraining = material.Id;

            // save and publish notifications
            var notification = new Domain.Entities.Notification
            {
                OwnerType = "topic",
                OwnerId = "all-all-all",
                Title = "[New] Content Training Material is Available",
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
                    Type = "training",
                    Key = "new_material",
                    NotificationType = FCMMessage.TYPE_TOPIC,
                    NotificationKey = "new_material", // only new_material
                }
            }, cancellationToken);

            // update master car
            var masterCar = await _context.MasterCars.FindAsync(material.MasterCarId);
            masterCar.TrainingLastUpdateDate = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateTrainingMaterialDto
            {
                Success = true,
                Message = "Training Material has been successfully created"
            };
        }
    }
}