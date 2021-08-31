using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.DeleteImageGallery
{
    public class DeleteImageGalleryCommandHandler : IRequestHandler<DeleteImageGalleryCommand, DeleteImageGalleryDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteImageGalleryCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteImageGalleryDto> Handle(DeleteImageGalleryCommand request, CancellationToken cancellationToken)
        {
            var ids = new List<int?>();
            foreach (var id in request.Ids)
            {
                var gallery = await _context.ImageGalleries
                    .Include(e => e.AdditionalInfoImageThumbnails)
                    .Include(e => e.BulletinInfoImageThumbnails)
                    .Include(e => e.MasterCarImageCovers)
                    .Include(e => e.MasterCarImageThumbnails)
                    .Include(e => e.MasterCarImageLogos)
                    .Include(e => e.TrainingMaterialImageThumbnails)
                    .Include(e => e.GuideMaterialImageThumbnails)
                    .Include(e => e.HomeBannerImages)
                    .Where(e => e.Id == id)
                    .FirstOrDefaultAsync();

                if (gallery != null 
                    && gallery.AdditionalInfoImageThumbnails.Count == 0
                    && gallery.BulletinInfoImageThumbnails.Count == 0
                    && gallery.MasterCarImageCovers.Count == 0
                    && gallery.MasterCarImageThumbnails.Count == 0
                    && gallery.MasterCarImageLogos.Count == 0
                    && gallery.GuideMaterialImageThumbnails.Count == 0
                    && gallery.TrainingMaterialImageThumbnails.Count == 0
                    && gallery.HomeBannerImages.Count == 0) 
                {
                    ids.Add(id);
                    _context.ImageGalleries.Remove(gallery);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeleteImageGalleryDto
            {
                Success = true,
                Message = "Image Gallery has been successfully deleted",
                DeletedIds = ids
                
            };
        }
    }


}