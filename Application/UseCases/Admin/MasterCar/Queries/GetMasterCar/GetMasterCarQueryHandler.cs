using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.MasterCar.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Queries.GetMasterCar
{
    public class GetMasterCarQueryHandler : IRequestHandler<GetMasterCarQuery, GetMasterCarDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetMasterCarQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetMasterCarDto> Handle(GetMasterCarQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.MasterCars
                .Include(e => e.ImageCover)
                .Include(e => e.ImageLogo)
                .Include(e => e.ImageThumbnail)
                .Where(e => e.Id == request.Id)
                .FirstAsync();

            var response = new GetMasterCarDto()
            {
                Success = true,
                Message = "Master Car is succefully retrieved"
            };

            if (category != null)
            {
                response.Data = _mapper.Map<MasterCarData>(category);
            }
            else
            {
                response.Success = false;
                response.Message = "Master Car is not found";
            }

            return response;
        }
    }
}
