using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Queries.GetInfo
{
    public class GetInfoQueryHandler : IRequestHandler<GetInfoQuery, GetInfoDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetInfoQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetInfoDto> Handle(GetInfoQuery request, CancellationToken cancellationToken)
        {
            var info = await _context.AdditionalInfos
                .Include(e => e.ImageThumbnail)
                .Where(e => e.Id == request.Id)
                .FirstAsync();
            var response = new GetInfoDto()
            {
                Success = true,
                Message = "Additional Info is successfully retrieved"
            };

            if (info != null)
            {
                info.TotalViews = info.TotalViews + 1;
                await _context.SaveChangesAsync(cancellationToken);
                response.Data = _mapper.Map<InfoData>(info);
            }
            else
            {
                response.Success = false;
                response.Message = "Additional Info not found";
            }   

            return new GetInfoDto()
            {
                Success = true,
                Message = "Additional Info is successfully retrieved",
                Data = _mapper.Map<InfoData>(info)
            };
        }
    }
}
