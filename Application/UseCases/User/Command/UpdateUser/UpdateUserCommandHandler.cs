using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using WebApi.Application.Interfaces;
using WebApi.Application.Misc;
using WebApi.Application.Exceptions;

namespace WebApi.Application.UseCases.User.Command.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;

        public UpdateUserCommandHandler(ISFDDBContext context, IUploader uploader)
        {
            _context = context;
            _uploader = uploader;
        }

        public async Task<UpdateUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var info = await _context.AdditionalInfos.FindAsync(request.Data.Id);
            if (info == null) throw new NotFoundException(nameof(Domain.Entities.AdditionalInfo), request.Data.Id);

            // validate file code
            var exists = await _context.AdditionalInfos
                .Where(e => e.FileCode.Equals(request.Data.FileCode))
                .Where(e => e.Id != request.Data.Id)
                .FirstOrDefaultAsync();
            
            if (exists != null) {
                return new UpdateUserDto
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

            return new UpdateUserDto
            {
                Success = true,
                Message = "Info has been successfully updated"
            };
        }
    }
}