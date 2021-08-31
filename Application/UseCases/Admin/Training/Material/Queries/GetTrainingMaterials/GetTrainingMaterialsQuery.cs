using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Queries.GetTrainingMaterials
{
    public class GetTrainingMaterialsQuery : AdminPaginationQuery, IRequest<GetTrainingMaterialsDto>
    {
        public IList<FilterParams> Filters { set; get; }
    }
}
