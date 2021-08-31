using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Exceptions;
using SFIDWebAPI.Application.UseCases.User.Bulletin.Models;

namespace SFIDWebAPI.Application.UseCases.User.Bulletin.Queries.GetBulletinDetail
{
    public class BulletinQueryHandler : IRequestHandler<BulletinDetailQuery, BulletinDetailDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public BulletinQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BulletinDetailDto> Handle(BulletinDetailQuery request, CancellationToken cancellationToken)
        {
            var bulletin = await _context
                .Bulletins
                .Include(e => e.ImageThumbnail)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
                

            if (bulletin == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Bulletin), request.Id);
            }

            // views counter
            bulletin.TotalViews = bulletin.TotalViews + 1;
            await _context.SaveChangesAsync(cancellationToken);

            return new BulletinDetailDto()
            {
                Success = true,
                Message = "Bulletin is succefully retrieved",
                Data = _mapper.Map<BulletinData>(bulletin)
            };
        }
    }
}
