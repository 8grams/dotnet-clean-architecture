using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.Training.Material.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Queries.GetTrainingMaterial
{
    public class GetTrainingMaterialQueryHandler : IRequestHandler<GetTrainingMaterialQuery, GetTrainingMaterialDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetTrainingMaterialQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetTrainingMaterialDto> Handle(GetTrainingMaterialQuery request, CancellationToken cancellationToken)
        {
            var material = await _context.TrainingMaterials
                .Include(e => e.ImageThumbnail)
                .Where(e => e.Id == request.Id)
                .FirstAsync();

            var response = new GetTrainingMaterialDto()
            {
                Success = true,
                Message = "Training Material is succefully retrieved"
            };

            if (material != null)
            {
                response.Data = _mapper.Map<TrainingMaterialData>(material);
            }
            else
            {
                response.Success = false;
                response.Message = "Training Material is not found";
            }

            return response;
        }
    }
}
