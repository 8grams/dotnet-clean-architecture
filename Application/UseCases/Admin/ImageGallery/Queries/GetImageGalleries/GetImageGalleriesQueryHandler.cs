using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Queries.GetImageGalleries
{
    public class GetImageGalleriesQueryHandler : IRequestHandler<GetImageGalleriesQuery, GetImageGalleriesDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetImageGalleriesQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetImageGalleriesDto> Handle(GetImageGalleriesQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.ImageGalleries.AsQueryable();

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Name.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.ImageGallery, ImageGalleryData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetImageGalleriesDto()
            {
                Success = true,
                Message = "Image Galleries are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }
    }
}
