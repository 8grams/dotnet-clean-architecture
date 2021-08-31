using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Command.DeleteRecommendation
{
    public class DeleteRecommendationCommand : BaseAdminQueryCommand, IRequest<DeleteRecommendationDto>
    {
        public IList<DeleteRecommendationData> Ids { set; get; }
    }

    public class DeleteRecommendationData
    {
        public string ContentType { set; get; }
        public int ContentId { set; get; }
    }
}