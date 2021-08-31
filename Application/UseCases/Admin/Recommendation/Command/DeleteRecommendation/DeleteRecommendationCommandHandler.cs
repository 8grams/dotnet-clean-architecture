using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Recommendation.Command.DeleteRecommendation
{
    public class DeleteRecommendationCommandHandler : IRequestHandler<DeleteRecommendationCommand, DeleteRecommendationDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteRecommendationCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteRecommendationDto> Handle(DeleteRecommendationCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var recommendation = await _context.Recommendations
                    .Where(e => e.ContentId == id.ContentId)
                    .Where(e => e.ContentType.Equals(id.ContentType))
                    .FirstAsync();

                if (recommendation != null) _context.Recommendations.Remove(recommendation);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeleteRecommendationDto
            {
                Success = true,
                Message = "Recommendation has been successfully deleted"
            };
        }
    }


}