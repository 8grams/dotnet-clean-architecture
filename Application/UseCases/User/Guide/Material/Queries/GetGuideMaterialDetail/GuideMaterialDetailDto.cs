using System;
using SFIDWebAPI.Application.UseCases.User.Guide.Material.Models;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Material.Queries.GetGuideMaterialDetail
{
    public class GuideMaterialDetailDto : BaseDto
    {
        public GuideMaterialData Data { set; get; }
    }
}
