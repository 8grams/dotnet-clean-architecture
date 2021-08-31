using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Extensions;
using SFIDWebAPI.Application.UseCases.User.Guide.Category.Models;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Category.Queries.GetGuideCategoryList
{
    public class GuideCategoryListQueryHandler : IRequestHandler<GuideCategoryListQuery, GuideCategoryListDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GuideCategoryListQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GuideCategoryListDto> Handle(GuideCategoryListQuery request, CancellationToken cancellationToken)
        {
            var data = _context.MasterCars
                .Include(e => e.ImageCover)
                .Include(e => e.ImageThumbnail)
                .Include(e => e.ImageLogo)
                .Where(e => e.GuideActive == true);

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.MasterCar, GuideCategoryData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GuideCategoryListDto()
            {
                Success = true,
                Message = "Guide Categories are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }
    }
}
