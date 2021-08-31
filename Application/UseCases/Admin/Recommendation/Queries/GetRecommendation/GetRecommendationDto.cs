using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.Recommendation.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Queries.GetRecommendation
{
    public class GetRecommendationDto : BaseDto
    {
        public RecommendationData Data { set; get; }
    }
}
