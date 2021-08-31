using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Exceptions;
using SFIDWebAPI.Application.UseCases.User.Guide.Material.Models;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Material.Queries.GetGuideMaterialDetail
{
    public class GuideMaterialDetailQueryHandler : IRequestHandler<GuideMaterialDetailQuery, GuideMaterialDetailDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GuideMaterialDetailQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GuideMaterialDetailDto> Handle(GuideMaterialDetailQuery request, CancellationToken cancellationToken)
        {
            var material = await _context
                .GuideMaterials
                .Include(e => e.ImageThumbnail)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
                
            if (material == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.GuideMaterial), request.Id);
            }

            // views counter
            material.TotalViews = material.TotalViews + 1;
            await _context.SaveChangesAsync(cancellationToken);

            return new GuideMaterialDetailDto()
            {
                Success = true,
                Message = "Guide Material is succefully retrieved",
                Data = _mapper.Map<GuideMaterialData>(material)
            };
        }
    }
}
