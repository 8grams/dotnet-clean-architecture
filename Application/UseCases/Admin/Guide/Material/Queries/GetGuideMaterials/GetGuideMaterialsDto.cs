using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Queries.GetGuideMaterials
{
    public class GetGuideMaterialsDto : PaginationDto
    {
        public IList<GuideMaterialData> Data { set; get; }
    }   
}
