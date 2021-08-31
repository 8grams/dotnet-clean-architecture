using SFIDWebAPI.Application.Models.Query;
using System.Collections.Generic;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.DeleteImageGallery
{
    public class DeleteImageGalleryDto : BaseDto
    {
        public IList<int?> DeletedIds { set; get; }
    }
}