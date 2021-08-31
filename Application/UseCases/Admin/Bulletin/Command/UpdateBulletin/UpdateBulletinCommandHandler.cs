using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;
using SFIDWebAPI.Application.Exceptions;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Command.UpdateBulletin
{
    public class UpdateBulletinCommandHandler : IRequestHandler<UpdateBulletinCommand, UpdateBulletinDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;

        public UpdateBulletinCommandHandler(ISFDDBContext context, IUploader uploader)
        {
            _context = context;
            _uploader = uploader;
        }

        public async Task<UpdateBulletinDto> Handle(UpdateBulletinCommand request, CancellationToken cancellationToken)
        {
            var bulletin = await _context.Bulletins.FindAsync(request.Data.Id);
            if (bulletin == null) throw new NotFoundException(nameof(Domain.Entities.Bulletin), request.Data.Id);

            // validate file code
            var exists = await _context.Bulletins
                .Where(e => e.FileCode.Equals(request.Data.FileCode))
                .Where(e => e.Id != request.Data.Id)
                .FirstOrDefaultAsync();
            
            if (exists != null) {
                return new UpdateBulletinDto
                {
                    Success = false,
                    Message = "File code already exists"
                };
            }

            bulletin.Title = request.Data.Title;
            bulletin.FileCode = request.Data.FileCode;
            bulletin.IsActive = request.Data.IsActive;
            bulletin.ImageThumbnailId = request.Data.ImageThumbnailId;
            bulletin.LastUpdateBy = request.AdminName;
            
            var recommendation = await _context.Recommendations
                .Where(e => e.ContentType.Equals(Domain.Entities.Recommendation.TYPE_BULLETIN))
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
                        ContentId = bulletin.Id,
                        ContentType = Domain.Entities.Recommendation.TYPE_BULLETIN,
                        PublishedAt = DateTime.Now,
                        ExpiresAt = DateTime.Now.AddMonths(1),
                        CreateBy = request.AdminName
                    });
                }
            }

            if (!string.IsNullOrEmpty(request.Data.FileByte) && !string.IsNullOrEmpty(request.Data.FileName))
            {
                var fileUrl = await _uploader.UploadFile(request.Data.FileByte, "bulletin", request.Data.FileName);
                bulletin.FileType = Utils.GetFileExtension(request.Data.FileName);
                bulletin.Link = fileUrl;
            }

            _context.Bulletins.Update(bulletin);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateBulletinDto
            {
                Success = true,
                Message = "Bulletin has been successfully updated"
            };
        }
    }
}