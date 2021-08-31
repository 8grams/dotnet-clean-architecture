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

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealerBranches
{
    public class GetDealerBranchesQueryHandler : IRequestHandler<GetDealerBranchesQuery, GetDealerBranchesDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetDealerBranchesQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetDealerBranchesDto> Handle(GetDealerBranchesQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.DealerBranches.AsQueryable();

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Name.Contains(@0)", request.QuerySearch);
            }

            foreach (var filter in request.Filters)
            {
                data = data.Where($"{filter.Column} == {filter.Value}");
            }

            var dealerBranches = await data
                .Take(10)
                .ProjectTo<DealerBranchData>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetDealerBranchesDto()
            {
                Success = true,
                Message = "Dealer Branches are succefully retrieved",
                Data = dealerBranches
            };
        }
    }
}
