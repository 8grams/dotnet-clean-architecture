using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.CreateImageGallery
{
    public class CreateImageGalleryCommandHandler : IRequestHandler<CreateImageGalleryCommand, CreateImageGalleryDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IUploader _uploader;

        public CreateImageGalleryCommandHandler(ISFDDBContext context, IUploader uploader)
        {
            _context = context;
            _uploader = uploader;
        }

        public async Task<CreateImageGalleryDto> Handle(CreateImageGalleryCommand request, CancellationToken cancellationToken)
        {
            var fileUrl = await _uploader.UploadFile(request.Data.FileByte, 
                "images", Utils.GenerateFileCode("image", "/Uploads/images/" + request.Data.Category + "-" + request.Data.Name) + "_" + request.Data.Name);

            var img = new Domain.Entities.ImageGallery {
                Name = request.Data.Name,
                Category = request.Data.Category,
                Link = fileUrl,
                CreateBy = request.AdminName
            };

            _context.ImageGalleries.Add(img);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateImageGalleryDto
            {
                Success = true,
                Message = "Image Gallery has been successfully created"
            };
        }
    }
}