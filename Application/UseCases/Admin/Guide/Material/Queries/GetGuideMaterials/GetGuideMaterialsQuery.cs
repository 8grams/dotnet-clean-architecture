using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Queries.GetGuideMaterials
{
    public class GetGuideMaterialsQuery : AdminPaginationQuery, IRequest<GetGuideMaterialsDto>
    {
        public IList<FilterParams> Filters { set; get; }
    }
}
