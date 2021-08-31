using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealers
{
    public class GetDealersQueryHandler : IRequestHandler<GetDealersQuery, GetDealersDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetDealersQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetDealersDto> Handle(GetDealersQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.Dealers.AsQueryable();

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("DealerName.Contains(@0)", request.QuerySearch.ToLower());
            }

            foreach (var filter in request.Filters)
            {
                data = data.Where($"{filter.Column} == {filter.Value}");
            }

            var dealers = await data
                .Take(10)
                .ProjectTo<DealerData>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetDealersDto()
            {
                Success = true,
                Message = "Dealers are succefully retrieved",
                Data = dealers
            };
        }
    }
}
