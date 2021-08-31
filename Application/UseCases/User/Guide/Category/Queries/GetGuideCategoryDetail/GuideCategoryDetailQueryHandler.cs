using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Exceptions;
using SFIDWebAPI.Application.UseCases.User.Guide.Category.Models;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Category.Queries.GetGuideCategoryDetail
{
    public class GuideCategoryDetailQueryHandler : IRequestHandler<GuideCategoryDetailQuery, GuideCategoryDetailDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GuideCategoryDetailQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GuideCategoryDetailDto> Handle(GuideCategoryDetailQuery request, CancellationToken cancellationToken)
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

            return new GuideCategoryDetailDto()
            {
                Success = true,
                Message = "Guide Category is succefully retrieved",
                Data = _mapper.Map<GuideCategoryData>(category)
            };
        }
    }
}
