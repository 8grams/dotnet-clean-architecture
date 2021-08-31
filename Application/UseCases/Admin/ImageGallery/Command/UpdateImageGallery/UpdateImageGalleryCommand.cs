using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.UpdateImageGallery
{
    public class UpdateImageGalleryCommand : BaseAdminQueryCommand, IRequest<UpdateImageGalleryDto>
    {
        public UpdateImageGalleryData Data { set; get; }
    }

    public class UpdateImageGalleryData
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Category { set; get; }
        public string FileByte { set; get; }
    }
}
