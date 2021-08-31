using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Command.DeleteMasterCar
{
    public class DeleteMasterCarCommand : BaseAdminQueryCommand, IRequest<DeleteMasterCarDto>
    {
        public IList<int?> Ids { set; get; }
    }
}