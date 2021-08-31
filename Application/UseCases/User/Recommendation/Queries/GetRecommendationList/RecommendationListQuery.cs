using System;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Recommendation.Queries.GetRecommendationList
{
    public class RecommendationListQuery : BaseQueryCommand, IRequest<RecommendationListDto>
    {
    }
}
