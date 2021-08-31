using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Command.DeleteInfo
{
    public class DeleteInfoCommand : BaseAdminQueryCommand, IRequest<DeleteInfoDto>
    {
        public IList<int?> Ids { set; get; }
    }
}