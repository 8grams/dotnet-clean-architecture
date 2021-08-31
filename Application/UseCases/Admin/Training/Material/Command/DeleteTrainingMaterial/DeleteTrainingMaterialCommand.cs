using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.DeleteTrainingMaterial
{
    public class DeleteTrainingMaterialCommand : BaseAdminQueryCommand, IRequest<DeleteTrainingMaterialDto>
    {
        public IList<int?> Ids { set; get; }
    }
}