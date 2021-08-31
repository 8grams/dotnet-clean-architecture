using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.User.AdditionalInfo.Queries.GetAdditionalInfoList
{
    public class AdditionalInfoListQueryHandler : IRequestHandler<AdditionalInfoListQuery, AdditionalInfoListDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryData _memory;

        public AdditionalInfoListQueryHandler(ISFDDBContext context, IMapper mapper, IMemoryData memory)
        {
            _context = context;
            _mapper = mapper;
            _memory = memory;
        }

        public async Task<AdditionalInfoListDto> Handle(AdditionalInfoListQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.AdditionalInfos
                .Include(e => e.ImageThumbnail)
                .Where(e => e.PublishedAt < now)
                .Where(e => e.ExpiresAt > now)
                .Where(e => e.IsActive == true);

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.AdditionalInfo, AdditionalInfoData>(request.PagingPage, request.PagingLimit, _mapper);

            var updates = await _context.MaterialStatuses
                .Where(e => e.UserId == request.UserId)
                .FirstOrDefaultAsync();
                
            if (updates != null)
            {
                updates.NewInfo = _memory.NewInfo;
                _context.MaterialStatuses.Update(updates);
                await _context.SaveChangesAsync(cancellationToken); 
            }

            return new AdditionalInfoListDto()
            {
                Success = true,
                Message = "Additional Info are succefully retrieved",
                Data = results.Data,
                Origin = "info.success.default",
                Pagination = results.Pagination
            };
        }
    }
}
