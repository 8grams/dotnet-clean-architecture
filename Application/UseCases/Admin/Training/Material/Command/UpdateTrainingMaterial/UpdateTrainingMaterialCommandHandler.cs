using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;
using SFIDWebAPI.Application.Exceptions;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.UpdateTrainingMaterial
{
    public class UpdateTrainingMaterialCommandHandler : IRequestHandler<UpdateTrainingMaterialCommand, UpdateTrainingMaterialDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;

        public UpdateTrainingMaterialCommandHandler(ISFDDBContext context, IUploader uploader)
        {
            _context = context;
            _uploader = uploader;
        }

        public async Task<UpdateTrainingMaterialDto> Handle(UpdateTrainingMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = await _context.TrainingMaterials.FindAsync(request.Data.Id);
            if (material == null) throw new NotFoundException(nameof(Domain.Entities.TrainingMaterial), request.Data.Id);

            // validate file code
            var exists = await _context.TrainingMaterials
                .Where(e => e.FileCode.Equals(request.Data.FileCode))
                .Where(e => e.Id != request.Data.Id)
                .FirstOrDefaultAsync();
            
            if (exists != null) {
                return new UpdateTrainingMaterialDto
                {
                    Success = false,
                    Message = "File code already exists"
                };
            }

            material.Title = request.Data.Title;
            material.FileCode = request.Data.FileCode;
            material.LastUpdateBy = request.AdminName;
            material.ImageThumbnailId = request.Data.ImageThumbnailId;

            if (!string.IsNullOrEmpty(request.Data.FileByte) && !string.IsNullOrEmpty(request.Data.FileName))
            {
                var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "training", request.Data.FileName);
                material.FileType = Utils.GetFileExtension(request.Data.FileName);
                material.Link = fileUrl;
            }

            var recommendation = await _context.Recommendations
                .Where(e => e.ContentType.Equals(Domain.Entities.Recommendation.TYPE_TRAINING))
                .Where(e => e.ContentId == request.Data.Id)
                .FirstOrDefaultAsync();

            if (!request.Data.IsRecommended)
            {
                if (recommendation != null)
                {
                    _context.Recommendations.Remove(recommendation);
                }
            }
            else
            {
                if (recommendation == null)
                {
                    _context.Recommendations.Add(new Domain.Entities.Recommendation
                    {
                        ContentId = material.Id,
                        ContentType = Domain.Entities.Recommendation.TYPE_TRAINING,
                        PublishedAt = DateTime.Now,
                        ExpiresAt = DateTime.Now.AddMonths(1)
                    });
                }
            }
            
            _context.TrainingMaterials.Update(material);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateTrainingMaterialDto
            {
                Success = true,
                Message = "Training Material has been successfully updated"
            };
        }
    }
}