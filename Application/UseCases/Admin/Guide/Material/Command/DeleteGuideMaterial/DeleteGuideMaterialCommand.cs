using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.DeleteGuideMaterial
{
    public class DeleteGuideMaterialCommand : BaseAdminQueryCommand, IRequest<DeleteGuideMaterialDto>
    {
        public IList<int?> Ids { set; get; }
    }
}