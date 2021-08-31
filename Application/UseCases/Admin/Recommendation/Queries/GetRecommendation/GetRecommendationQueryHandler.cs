using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.Recommendation.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Queries.GetRecommendation
{
    public class GetRecommendationQueryHandler : IRequestHandler<GetRecommendationQuery, GetRecommendationDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetRecommendationQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetRecommendationDto> Handle(GetRecommendationQuery request, CancellationToken cancellationToken)
        {
            var recommendation = await _context.Recommendations
                .Where(e => e.ContentId == request.ContentId)
                .Where(e => e.ContentType.Equals(request.ContentType))
                .FirstAsync();

            var data = _mapper.Map<RecommendationData>(RecommendationData.GetRecommendationContent(_context, recommendation.ContentType, recommendation.ContentId));
            data.Type = request.ContentType;

            await _context.SaveChangesAsync(cancellationToken);
            return new GetRecommendationDto()
            {
                Success = true,
                Message = "Recommendation is succefully retrieved",
                Data = data
            };
        }
    }
}
