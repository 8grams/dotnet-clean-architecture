using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Command.CreateRecommendation
{
    public class CreateRecommendationCommand : BaseAdminQueryCommand, IRequest<CreateRecommendationDto>
    {
        public CreateRecommendationData Data { set; get; }
    }

    public class CreateRecommendationData
    {
        public string ContentType { set; get; }
        public int ContentId { set; get; }
    }
}
