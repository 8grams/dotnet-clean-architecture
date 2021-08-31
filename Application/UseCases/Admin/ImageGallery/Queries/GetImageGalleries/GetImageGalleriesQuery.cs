using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Queries.GetImageGalleries
{
    public class GetImageGalleriesQuery : AdminPaginationQuery, IRequest<GetImageGalleriesDto>
    {
    }
}
