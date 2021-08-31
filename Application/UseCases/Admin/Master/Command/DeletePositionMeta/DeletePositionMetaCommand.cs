using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Command.DeletePositionMeta
{
    public class DeletePositionMetaCommand : BaseAdminQueryCommand, IRequest<DeletePositionMetaDto>
    {
        public IList<int?> Ids { set; get; }
    }
}