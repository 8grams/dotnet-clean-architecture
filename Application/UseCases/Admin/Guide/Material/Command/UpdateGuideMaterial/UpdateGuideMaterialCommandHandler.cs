using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;
using SFIDWebAPI.Application.Exceptions;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.UpdateGuideMaterial
{
    public class UpdateGuideMaterialCommandHandler : IRequestHandler<UpdateGuideMaterialCommand, UpdateGuideMaterialDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;

        public UpdateGuideMaterialCommandHandler(ISFDDBContext context, IUploader uploader)
        {
            _context = context;
            _uploader = uploader;
        }

        public async Task<UpdateGuideMaterialDto> Handle(UpdateGuideMaterialCommand request, CancellationToken cancellationToken)
        {
            var material = await _context.GuideMaterials.FindAsync(request.Data.Id);
            if (material == null) throw new NotFoundException(nameof(Domain.Entities.GuideMaterial), request.Data.Id);

            // validate file code
            var exists = await _context.GuideMaterials
                .Where(e => e.FileCode.Equals(request.Data.FileCode))
                .Where(e => e.Id != request.Data.Id)
                .FirstOrDefaultAsync();
            
            if (exists != null) {
                return new UpdateGuideMaterialDto
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
                var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "guide", request.Data.FileName);
                material.FileType = Utils.GetFileExtension(request.Data.FileName);
                material.Link = fileUrl;
            }

            var recommendation = await _context.Recommendations
                .Where(e => e.ContentType.Equals(Domain.Entities.Recommendation.TYPE_GUIDE))
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
                        ContentType = Domain.Entities.Recommendation.TYPE_GUIDE,
                        PublishedAt = DateTime.Now,
                        ExpiresAt = DateTime.Now.AddMonths(1)
                    });
                }
            }
   
            _context.GuideMaterials.Update(material);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateGuideMaterialDto
            {
                Success = true,
                Message = "Guide Material has been successfully updated"
            };
        }
    }
}