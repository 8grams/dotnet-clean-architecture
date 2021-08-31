using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Linq;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Extensions;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.User.PKT.History.Queries.GetHistoryList
{
    public class PKTHistoryListQueryHandler : IRequestHandler<PKTHistoryListQuery, PKTHistoryListDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public PKTHistoryListQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PKTHistoryListDto> Handle(PKTHistoryListQuery request, CancellationToken cancellationToken)
        {
            var data = _context.PKTHistories
                .Include(e => e.Salesman)
                .Where(e => e.SalesCode.Equals(request.SalesCode));
            
            if(!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Vin.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.PKTHistory, PKTHistoryData>(request.PagingPage, request.PagingLimit, _mapper);

            return new PKTHistoryListDto()
            {
                Success = true,
                Message = "PKT Histories are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }
    }
}
