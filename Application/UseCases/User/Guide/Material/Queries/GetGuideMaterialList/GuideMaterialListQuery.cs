using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Material.Queries.GetGuideMaterialList
{
    public class GuideMaterialListQuery : PaginationQuery, IRequest<GuideMaterialListDto>
    {
        public IList<string> Includes { set; get; }
        public IList<FilterParams> Filters { set; get; }
    }
}
