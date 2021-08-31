using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.MasterCar.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Queries.GetMasterCars
{
    public class GetMasterCarsQueryHandler : IRequestHandler<GetMasterCarsQuery, GetMasterCarsDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetMasterCarsQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetMasterCarsDto> Handle(GetMasterCarsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.MasterCars
                .Include(e => e.ImageCover)
                .Include(e => e.ImageThumbnail)
                .Include(e => e.ImageLogo)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0) || Tag.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.MasterCar, MasterCarData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetMasterCarsDto()
            {
                Success = true,
                Message = "Master Cars are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }
    }
}
