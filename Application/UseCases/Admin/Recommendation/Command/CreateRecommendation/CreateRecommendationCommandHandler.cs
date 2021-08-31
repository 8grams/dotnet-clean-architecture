using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.Recommendation.Models;
using SFIDWebAPI.Application.Exceptions;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Command.CreateRecommendation
{
    public class CreateRecommendationCommandHandler : IRequestHandler<CreateRecommendationCommand, CreateRecommendationDto>
    {
        private readonly ISFDDBContext _context;

        public CreateRecommendationCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<CreateRecommendationDto> Handle(CreateRecommendationCommand request, CancellationToken cancellationToken)
        {
            // ensure data is exist
            var exist = await RecommendationData.GetRecommendationContent(_context, request.Data.ContentType, request.Data.ContentId);
            if (exist == null) throw new InvalidOperationException("Data not found");

            var recommendation = new Domain.Entities.Recommendation
            {
                ContentType = request.Data.ContentType,
                ContentId = request.Data.ContentId,
                PublishedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddYears(1)
            };

            _context.Recommendations.Add(recommendation);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateRecommendationDto
            {
                Success = true,
                Message = "Recommendation has been successfully created"
            };
        }
    }
}