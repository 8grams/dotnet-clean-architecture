using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Exceptions;
using SFIDWebAPI.Application.UseCases.User.Training.Category.Models;

namespace SFIDWebAPI.Application.UseCases.User.Training.Category.Queries.GetTrainingCategoryDetail
{
    public class TrainingCategoryDetailQueryHandler : IRequestHandler<TrainingCategoryDetailQuery, TrainingCategoryDetailDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public TrainingCategoryDetailQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TrainingCategoryDetailDto> Handle(TrainingCategoryDetailQuery request, CancellationToken cancellationToken)
        {
            var category = await _context
                .MasterCars
                .Include(e => e.ImageCover)
                .Include(e => e.ImageThumbnail)
                .Include(e => e.ImageLogo)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();        

            if (category == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.MasterCar), request.Id);
            }

            return new TrainingCategoryDetailDto()
            {
                Success = true,
                Message = "Training Category is succefully retrieved",
                Data = _mapper.Map<TrainingCategoryData>(category)
            };
        }
    }
}
