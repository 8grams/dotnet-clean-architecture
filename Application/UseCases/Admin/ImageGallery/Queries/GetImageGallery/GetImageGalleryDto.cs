using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Queries.GetImageGallery
{
    public class GetImageGalleryDto : BaseDto
    {
        public ImageGalleryData Data { set; get; }
    }   
}
