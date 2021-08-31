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

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetSalesmen
{
    public class GetSalesmenQueryHandler : IRequestHandler<GetSalesmenQuery, GetSalesmenDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetSalesmenQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetSalesmenDto> Handle(GetSalesmenQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.Salesmen.AsQueryable();

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("SalesmanCode.Contains(@0) || SalesmanName.Contains(@0)", request.QuerySearch);
            }

            var salesment = await data
                .Take(10)
                .ProjectTo<SalesmanData>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetSalesmenDto()
            {
                Success = true,
                Message = "Salesmen are succefully retrieved",
                Data = salesment
            };
        }
    }
}
