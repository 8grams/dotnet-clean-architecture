using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Queries.GetAdmins
{
    public class GetAdminsQueryHandler : IRequestHandler<GetAdminsQuery, GetAdminsDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetAdminsQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAdminsDto> Handle(GetAdminsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.Admins.AsQueryable();

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Name.Contains(@0) || Email.Contains(@0) || Password.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.Admin, AdminData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetAdminsDto()
            {
                Success = true,
                Message = "Admins are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };

        }
    }
}
