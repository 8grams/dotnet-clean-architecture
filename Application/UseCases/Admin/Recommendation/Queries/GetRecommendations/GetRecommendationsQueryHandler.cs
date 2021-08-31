using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.Recommendation.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Queries.GetRecommendations
{
    public class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, GetRecommendationsDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;


        public GetRecommendationsQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetRecommendationsDto> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var records = await _context.Recommendations
                .Where(e => e.PublishedAt < now)
                .Where(e => e.ExpiresAt > now)
                .ToListAsync();

            var recommendations = new List<RecommendationData>();
            foreach(var item in records)
            {
                var recommendable = await RecommendationData.GetRecommendationContent(_context, item.ContentType, item.ContentId);
                if (recommendable != null)
                {
                    recommendations.Add(new RecommendationData
                    {
                        Id = item.ContentId,
                        Title = recommendable.Title,
                        Type = item.ContentType,
                    });
                }
            }

            return new GetRecommendationsDto()
            {
                Success = true,
                Message = "Recommendations are succefully retrieved",
                Data = recommendations,
            };

        }
    }
}
