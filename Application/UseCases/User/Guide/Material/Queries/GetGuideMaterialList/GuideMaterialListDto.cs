using System;
using System.Collections.Generic;
using SFIDWebAPI.Application.UseCases.User.Guide.Material.Models;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Material.Queries.GetGuideMaterialList
{
    public class GuideMaterialListDto : PaginationDto
    {
        public IEnumerable<GuideMaterialData> Data { set; get; }
    }
}
