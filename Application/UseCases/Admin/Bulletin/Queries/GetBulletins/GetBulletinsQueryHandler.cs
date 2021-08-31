using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.Bulletin.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Queries.GetBulletins
{
    public class GetBulletinsQueryHandler : IRequestHandler<GetBulletinsQuery, GetBulletinsDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetBulletinsQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetBulletinsDto> Handle(GetBulletinsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.Bulletins
                .Include(e => e.ImageThumbnail)
                .AsQueryable();
            
            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0) || FileCode.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.Bulletin, BulletinData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetBulletinsDto()
            {
                Success = true,
                Message = "Bulletins are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };

        }
    }
}
