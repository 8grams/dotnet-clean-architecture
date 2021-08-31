using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;
using SFIDWebAPI.Application.Exceptions;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Command.UpdateInfo
{
    public class UpdateInfoCommandHandler : IRequestHandler<UpdateInfoCommand, UpdateInfoDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;

        public UpdateInfoCommandHandler(ISFDDBContext context, IUploader uploader)
        {
            _context = context;
            _uploader = uploader;
        }

        public async Task<UpdateInfoDto> Handle(UpdateInfoCommand request, CancellationToken cancellationToken)
        {
            var info = await _context.AdditionalInfos.FindAsync(request.Data.Id);
            if (info == null) throw new NotFoundException(nameof(Domain.Entities.AdditionalInfo), request.Data.Id);

            // validate file code
            var exists = await _context.AdditionalInfos
                .Where(e => e.FileCode.Equals(request.Data.FileCode))
                .Where(e => e.Id != request.Data.Id)
                .FirstOrDefaultAsync();
            
            if (exists != null) {
                return new UpdateInfoDto
                {
                    Success = false,
                    Message = "File code already exists"
                };
            }

            info.Title = request.Data.Title;
            info.FileCode = request.Data.FileCode;
            info.ImageThumbnailId = request.Data.ImageThumbnailId;
            info.IsActive = request.Data.IsActive;
            info.IsDownloadable = request.Data.IsDownloadable;
            info.LastUpdateBy = request.AdminName;
            
            var recommendation = await _context.Recommendations
                .Where(e => e.ContentType.Equals(Domain.Entities.Recommendation.TYPE_INFO))
                .Where(e => e.ContentId == request.Data.Id)
                .FirstOrDefaultAsync();

            if (!request.Data.IsRecommended)
            {
                // delete from recommendation
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
                        ContentId = info.Id,
                        ContentType = Domain.Entities.Recommendation.TYPE_INFO,
                        PublishedAt = DateTime.Now,
                        ExpiresAt = DateTime.Now.AddMonths(1),
                        CreateBy = request.AdminName
                    });
                }
            }

            if (!string.IsNullOrEmpty(request.Data.FileByte) && !string.IsNullOrEmpty(request.Data.FileName))
            {
                var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "info", request.Data.FileName);
                info.FileType = Utils.GetFileExtension(request.Data.FileName);
                info.Link = fileUrl;
            }

            _context.AdditionalInfos.Update(info);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateInfoDto
            {
                Success = true,
                Message = "Info has been successfully updated"
            };
        }
    }
}