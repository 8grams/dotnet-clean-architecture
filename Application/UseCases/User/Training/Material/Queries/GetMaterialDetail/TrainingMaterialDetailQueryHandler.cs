using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Exceptions;
using SFIDWebAPI.Application.UseCases.User.Training.Material.Models;

namespace SFIDWebAPI.Application.UseCases.User.Training.Material.Queries.GetTrainingMaterialDetail
{
    public class TrainingMaterialDetailQueryHandler : IRequestHandler<TrainingMaterialDetailQuery, TrainingMaterialDetailDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public TrainingMaterialDetailQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TrainingMaterialDetailDto> Handle(TrainingMaterialDetailQuery request, CancellationToken cancellationToken)
        {
            var material = await _context.TrainingMaterials
                .Where(e => e.Id == request.Id)
                .Include(e => e.ImageThumbnail)
                .FirstOrDefaultAsync();
            if (material == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.TrainingMaterial), request.Id);
            }

            // views counter
            material.TotalViews = material.TotalViews + 1;
            await _context.SaveChangesAsync(cancellationToken);

            return new TrainingMaterialDetailDto()
            {
                Success = true,
                Message = "Material is succefully retrieved",
                Data = _mapper.Map<TrainingMaterialData>(material)
            };
        }
    }
}
