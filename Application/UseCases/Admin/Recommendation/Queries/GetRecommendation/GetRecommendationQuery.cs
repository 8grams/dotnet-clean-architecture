using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Queries.GetRecommendation
{
    public class GetRecommendationQuery : BaseAdminQueryCommand, IRequest<GetRecommendationDto>
    {
        public int ContentId { set; get; }
        public string ContentType { set; get; }
    }
}
