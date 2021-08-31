using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.CreateImageGallery
{
    public class CreateImageGalleryCommand : BaseAdminQueryCommand, IRequest<CreateImageGalleryDto>
    {
        public CreateImageGalleryData Data { set; get; }
    }

    public class CreateImageGalleryData
    {
        public string Name { set; get; }
        public string Category { set; get; }
        public string FileByte { set; get; }
    }
}
