using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.DeleteImageGallery
{
    public class DeleteImageGalleryCommand : BaseAdminQueryCommand, IRequest<DeleteImageGalleryDto>
    {
        public IList<int?> Ids { set; get; }
    }
}