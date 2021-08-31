using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Queries.GetInfos
{
    public class GetInfosQueryHandler : IRequestHandler<GetInfosQuery, GetInfosDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetInfosQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetInfosDto> Handle(GetInfosQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.AdditionalInfos
                .Include(e => e.ImageThumbnail)
                .AsQueryable();
            
            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0) || FileCode.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.AdditionalInfo, InfoData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetInfosDto()
            {
                Success = true,
                Message = "Infos are successfully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };

        }
    }
}
