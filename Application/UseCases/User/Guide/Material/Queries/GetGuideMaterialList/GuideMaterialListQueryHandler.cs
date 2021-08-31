using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.User.Guide.Material.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Material.Queries.GetGuideMaterialList
{
    public class GuideMaterialListQueryHandler : IRequestHandler<GuideMaterialListQuery, GuideMaterialListDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryData _memory;

        public GuideMaterialListQueryHandler(ISFDDBContext context, IMapper mapper, IMemoryData memory)
        {
            _context = context;
            _mapper = mapper;
            _memory = memory;
        }

        public async Task<GuideMaterialListDto> Handle(GuideMaterialListQuery request, CancellationToken cancellationToken)
        {
            IList<GuideMaterialData> videos = await this.getVideoMaterial(request);
            IList<GuideMaterialData> docs = await this.getDocsMaterial(request);

            var results = docs.Union(videos);
            
            var updates = await _context.MaterialStatuses
                .Where(e => e.UserId == request.UserId)
                .FirstOrDefaultAsync();
                
            if (updates != null)
            {
                updates.NewGuide = _memory.NewGuide;
                _context.MaterialStatuses.Update(updates);
                await _context.SaveChangesAsync(cancellationToken); 
            }

            // set category counter
            foreach (var filter in request.Filters)
            {
                if (filter.Column.Equals("MasterCarId"))
                {
                    _context.MaterialCounters.Add(new Domain.Entities.MaterialCounter
                    {
                        ContentId = int.Parse(filter.Value),
                        ContentType = "guide-category",
                        UserId = request.UserId
                    });

                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            return new GuideMaterialListDto()
            {
                Success = true,
                Message = "Guide Materials are succefully retrieved",
                Data = results
            };
        }

        private async Task<IList<GuideMaterialData>> getDocsMaterial(GuideMaterialListQuery request)
        {
            var now = DateTime.Now;
            var data = _context.GuideMaterials
                .Include(e => e.ImageThumbnail)
                .Where(e => !e.FileType.Equals("mp4"))
                .Where(e => e.PublishedAt < now)
                .Where(e => e.ExpiresAt > now);
                
            foreach (var filter in request.Filters)
            {
                data = data.Where($"{filter.Column} == {filter.Value}");
            }

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0)", request.QuerySearch);
            }

            if (request.Includes.Contains("category"))
            {
                data.Include(e => e.MasterCarId);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.GuideMaterial, GuideMaterialData>(request.PagingPage, request.PagingLimit, _mapper);

            return results.Data;
        }

        private async Task<IList<GuideMaterialData>> getVideoMaterial(GuideMaterialListQuery request)
        {
            var now = DateTime.Now;
            var data = _context.GuideMaterials
                .Include(e => e.ImageThumbnail)
                .Where(e => e.FileType.Equals("mp4"))
                .Where(e => e.PublishedAt < now)
                .Where(e => e.ExpiresAt > now);
                
            foreach (var filter in request.Filters)
            {
                data = data.Where($"{filter.Column} == {filter.Value}");
            }

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0)", request.QuerySearch);
            }

            if (request.Includes.Contains("category"))
            {
                data.Include(e => e.MasterCarId);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.GuideMaterial, GuideMaterialData>(request.PagingPage, request.PagingLimit, _mapper);

            return results.Data;
        }
    }
}
