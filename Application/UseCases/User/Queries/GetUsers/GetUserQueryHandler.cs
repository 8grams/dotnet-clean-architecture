using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper;
using WebApi.Application.Interfaces;
using WebApi.Application.UseCases.User.Models;
using WebApi.Application.Extensions;

namespace WebApi.Application.UseCases.User.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersDto>
    {
        private readonly IWebApiDBContext _context;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IWebApiDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUsersDto> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var data = _context.Users
                .AsQueryable();
            
            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Name.Contains(@0) || Username.Contains(@0) || Email.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.User, UserData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetUsersDto()
            {
                Success = true,
                Message = "Users are successfully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };

        }
    }
}
