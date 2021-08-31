using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.Recommendation.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Queries.GetRecommendations
{
    public class GetRecommendationsDto : BaseDto
    {
        public IList<RecommendationData> Data { set; get; }
    }
}
