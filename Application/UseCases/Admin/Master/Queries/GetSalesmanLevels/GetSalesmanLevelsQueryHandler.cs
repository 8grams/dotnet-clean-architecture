using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetSalesmanLevels
{
    public class GetSalesmanLevelsQueryHandler : IRequestHandler<GetSalesmanLevelsQuery, GetSalesmanLevelsDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetSalesmanLevelsQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetSalesmanLevelsDto> Handle(GetSalesmanLevelsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = await _context.SalesmanLevels
                .ProjectTo<SalesmanLevelData>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetSalesmanLevelsDto()
            {
                Success = true,
                Message = "Salesman Levels are succefully retrieved",
                Data = data
            };
        }
    }
}
