using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Queries.GetGuideMaterial
{
    public class GetGuideMaterialDto : BaseDto
    {
        public GuideMaterialData Data { set; get; }
    }   
}
