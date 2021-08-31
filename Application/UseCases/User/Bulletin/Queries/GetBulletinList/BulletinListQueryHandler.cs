using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.User.Bulletin.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.User.Bulletin.Queries.GetBulletinList
{
    public class BulletinListQueryHandler : IRequestHandler<BulletinListQuery, BulletinListDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryData _memory;


        public BulletinListQueryHandler(ISFDDBContext context, IMapper mapper, IMemoryData memory)
        {
            _context = context;
            _mapper = mapper;
            _memory = memory;
        }

        public async Task<BulletinListDto> Handle(BulletinListQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.Bulletins
                .Include(e => e.ImageThumbnail)
                .Where(e => e.PublishedAt < now)
                .Where(e => e.ExpiresAt > now)
                .Where(e => e.IsActive == true);
                
            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0)", request.QuerySearch);
            }

            foreach (var filter in request.Filters)
            {   
                if (filter.Column == "CreateYear")
                {
                    data = data.Where($"CreateDate.Year == {int.Parse(filter.Value)}");
                }
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.Bulletin, BulletinData>(request.PagingPage, request.PagingLimit, _mapper);

            var updates = await _context.MaterialStatuses
                .Where(e => e.UserId == request.UserId)
                .FirstOrDefaultAsync();
                
            if (updates != null)
            {
                updates.NewBulletin = _memory.NewBulletin;
                _context.MaterialStatuses.Update(updates);
                await _context.SaveChangesAsync(cancellationToken); 
            }

            return new BulletinListDto()
            {
                Success = true,
                Message = "Bulletins are succefully retrieved",
                Data = results.Data,
                AvailableYears = request.PagingPage == 1 ? this._getYears() : new List<int>(),
                Pagination = results.Pagination
            };
        }

        private List<int> _getYears() {
            var years = _context.Bulletins
                .OrderByDescending(e => e.CreateDate)
                .Select(e => e.CreateDate.Year)
                .Distinct()
                .ToList();
            return years;
        }
    }
}
