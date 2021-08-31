using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Extensions;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.User.HomeBanner.Queries.GetHomeBannerList
{
    public class HomeBannerListQueryHandler : IRequestHandler<HomeBannerListQuery, HomeBannerListDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public HomeBannerListQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HomeBannerListDto> Handle(HomeBannerListQuery request, CancellationToken cancellationToken)
        {
            var data = _context.HomeBanners;

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.HomeBanner, HomeBannerData>(request.PagingPage, request.PagingLimit, _mapper);

            return new HomeBannerListDto()
            {
                Success = true,
                Message = "HomeBanner are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }
    }
}
