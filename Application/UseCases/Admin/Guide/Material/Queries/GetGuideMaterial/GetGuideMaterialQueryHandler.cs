using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Queries.GetGuideMaterial
{
    public class GetGuideMaterialQueryHandler : IRequestHandler<GetGuideMaterialQuery, GetGuideMaterialDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetGuideMaterialQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetGuideMaterialDto> Handle(GetGuideMaterialQuery request, CancellationToken cancellationToken)
        {
            var material = await _context.GuideMaterials
                .Include(e => e.ImageThumbnail)
                .Where(e => e.Id == request.Id)
                .FirstAsync();

            var response = new GetGuideMaterialDto()
            {
                Success = true,
                Message = "Guide Material is succefully retrieved"
            };

            if (material != null)
            {
                response.Data = _mapper.Map<GuideMaterialData>(material);
            }
            else
            {
                response.Success = false;
                response.Message = "Guide Material is not found";
            }

            return response;
        }
    }
}
