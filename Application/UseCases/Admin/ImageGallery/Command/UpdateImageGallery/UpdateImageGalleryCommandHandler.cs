using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;
using SFIDWebAPI.Application.Exceptions;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.UpdateImageGallery
{
    public class UpdateImageGalleryCommandHandler : IRequestHandler<UpdateImageGalleryCommand, UpdateImageGalleryDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;

        public UpdateImageGalleryCommandHandler(ISFDDBContext context, IUploader uploader)
        {
            _context = context;
            _uploader = uploader;
        }
        public async Task<UpdateImageGalleryDto> Handle(UpdateImageGalleryCommand request, CancellationToken cancellationToken)
        {
            var img = await _context.ImageGalleries.FindAsync(request.Data.Id);
            if (img == null) throw new NotFoundException(nameof(Domain.Entities.ImageGallery), request.Data.Id);

            if (!string.IsNullOrEmpty(request.Data.FileByte))
            {
                var fileUrl = await _uploader.UploadFile(request.Data.FileByte, 
                    "images", Utils.GenerateFileCode("image", "/Uploads/images/" + request.Data.Category + "-" + request.Data.Name) + "_" + request.Data.Name);
                img.Link = fileUrl;
            }

            _context.ImageGalleries.Update(img);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateImageGalleryDto
            {
                Success = true,
                Message = "Image Gallery has been successfully updated"
            };
        }
    }
}