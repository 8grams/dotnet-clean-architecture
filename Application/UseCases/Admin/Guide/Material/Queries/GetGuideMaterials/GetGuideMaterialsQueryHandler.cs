using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Queries.GetGuideMaterials
{
    public class GetGuideMaterialsQueryHandler : IRequestHandler<GetGuideMaterialsQuery, GetGuideMaterialsDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;


        public GetGuideMaterialsQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetGuideMaterialsDto> Handle(GetGuideMaterialsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.GuideMaterials
                .Include(e => e.ImageThumbnail)
                .AsQueryable();

            foreach (var filter in request.Filters)
            {
                data = data.Where($"{filter.Column} == {filter.Value}");
            }

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0) || FileCode.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.GuideMaterial, GuideMaterialData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetGuideMaterialsDto()
            {
                Success = true,
                Message = "Guide Materials are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }
    }
}
