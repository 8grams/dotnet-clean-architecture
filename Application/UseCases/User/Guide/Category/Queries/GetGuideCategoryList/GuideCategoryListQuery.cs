using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Category.Queries.GetGuideCategoryList
{
    public class GuideCategoryListQuery : PaginationQuery, IRequest<GuideCategoryListDto>
    {
        public IList<string> Includes { set; get; }
    }
}
