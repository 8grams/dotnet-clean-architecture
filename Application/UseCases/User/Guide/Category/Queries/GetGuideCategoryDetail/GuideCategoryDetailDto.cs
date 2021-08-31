using SFIDWebAPI.Application.UseCases.User.Guide.Category.Models;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Category.Queries.GetGuideCategoryDetail
{
    public class GuideCategoryDetailDto : BaseDto
    {
        public GuideCategoryData Data { set; get; }
    }
}
