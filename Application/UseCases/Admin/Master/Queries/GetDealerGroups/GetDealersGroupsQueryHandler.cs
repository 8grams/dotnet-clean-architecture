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

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealerGroups
{
    public class GetDealerGroupsQueryHandler : IRequestHandler<GetDealerGroupsQuery, GetDealerGroupsDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetDealerGroupsQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetDealerGroupsDto> Handle(GetDealerGroupsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.DealerGroups.AsQueryable();

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("GroupName.Contains(@0)", request.QuerySearch.ToLower());
            }

            var groups = await data
                .Take(10)
                .ProjectTo<DealerGroupData>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetDealerGroupsDto()
            {
                Success = true,
                Message = "Dealer Groups are succefully retrieved",
                Data = groups
            };
        }
    }
}
