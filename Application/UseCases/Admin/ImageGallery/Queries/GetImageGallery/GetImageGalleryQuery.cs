using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Queries.GetImageGallery
{
    public class GetImageGalleryQuery : BaseAdminQueryCommand, IRequest<GetImageGalleryDto>
    {
        public int Id { set; get; }
    }
}
