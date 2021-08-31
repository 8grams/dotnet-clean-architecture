using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetCities
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, GetCitiesDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetCitiesDto> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.Cities.AsQueryable();

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("CityName.Contains(@0)", request.QuerySearch.ToLower());
            }

            var cities = await data
                .Take(10)
                .ProjectTo<CityData>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetCitiesDto()
            {
                Success = true,
                Message = "Cities are succefully retrieved",
                Data = cities
            };
        }
    }
}
