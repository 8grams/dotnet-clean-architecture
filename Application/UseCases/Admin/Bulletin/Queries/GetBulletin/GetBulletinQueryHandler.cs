using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.Bulletin.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Queries.GetBulletin
{
    public class GetBulletinQueryHandler : IRequestHandler<GetBulletinQuery, GetBulletinDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetBulletinQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetBulletinDto> Handle(GetBulletinQuery request, CancellationToken cancellationToken)
        {
            var bulletin = await _context.Bulletins
                .Include(e => e.ImageThumbnail)
                .Where(e => e.Id == request.Id)
                .FirstAsync();

            var response = new GetBulletinDto()
            {
                Success = true,
                Message = "Bulletin is succefully retrieved"
            };
            
            // views counter
            if (bulletin != null)
            {
                bulletin.TotalViews = bulletin.TotalViews + 1;
                await _context.SaveChangesAsync(cancellationToken);
                response.Data = _mapper.Map<BulletinData>(bulletin);
            }
            else
            {
                response.Success = false;
                response.Message = "Bulletin not found";
            }

            return response;
        }
    }
}
