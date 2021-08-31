using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Queries.GetHomeBanner
{
    public class GetHomeBannerQueryHandler : IRequestHandler<GetHomeBannerQuery, GetHomeBannerDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetHomeBannerQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetHomeBannerDto> Handle(GetHomeBannerQuery request, CancellationToken cancellationToken)
        {
            var banner = await _context.HomeBanners
                .Include(e => e.Image)
                .Where(e => e.Id == request.Id)
                .FirstAsync();
            var response = new GetHomeBannerDto()
            {
                Success = true,
                Message = "Home Banner is succefully retrieved"
            };

            if (banner != null)
            {
                response.Data = _mapper.Map<HomeBannerData>(banner);
            }
            else
            {
                response.Success = false;
                response.Message = "Home Banner is not found";
            }

            return response;
        }
    }
}
