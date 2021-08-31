using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Queries.GetImageGalleries
{
    public class GetImageGalleriesDto : PaginationDto
    {
        public IList<ImageGalleryData> Data { set; get; }
    }   
}
