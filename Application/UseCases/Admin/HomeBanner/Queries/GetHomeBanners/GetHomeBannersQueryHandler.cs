using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Queries.GetHomeBanners
{
    public class GetHomeBannersQueryHandler : IRequestHandler<GetHomeBannersQuery, GetHomeBannersDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetHomeBannersQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetHomeBannersDto> Handle(GetHomeBannersQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.HomeBanners
                .Include(e => e.Image)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Name.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.HomeBanner, HomeBannerData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetHomeBannersDto()
            {
                Success = true,
                Message = "Home Banners are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }
    }
}
