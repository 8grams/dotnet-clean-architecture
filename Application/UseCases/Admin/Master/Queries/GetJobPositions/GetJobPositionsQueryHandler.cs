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

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetJobPositions
{
    public class GetJobPositionsQueryHandler : IRequestHandler<GetJobPositionsQuery, GetJobPositionsDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetJobPositionsQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetJobPositionsDto> Handle(GetJobPositionsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.JobPositions.AsQueryable();

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Description.Contains(@0)", request.QuerySearch.ToLower());
            }

            var dealers = await data
                .Take(10)
                .ProjectTo<JobPositionData>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetJobPositionsDto()
            {
                Success = true,
                Message = "Job Positions are succefully retrieved",
                Data = dealers
            };
        }
    }
}
