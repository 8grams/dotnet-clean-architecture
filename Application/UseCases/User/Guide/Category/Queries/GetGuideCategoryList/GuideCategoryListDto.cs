using System.Collections.Generic;
using SFIDWebAPI.Application.UseCases.User.Guide.Category.Models;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Category.Queries.GetGuideCategoryList
{
    public class GuideCategoryListDto : PaginationDto
    {
        public IList<GuideCategoryData> Data { set; get; }
    }
}
