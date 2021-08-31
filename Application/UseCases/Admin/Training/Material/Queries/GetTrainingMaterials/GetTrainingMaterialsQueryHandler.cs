using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.Training.Material.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Queries.GetTrainingMaterials
{
    public class GetTrainingMaterialsQueryHandler : IRequestHandler<GetTrainingMaterialsQuery, GetTrainingMaterialsDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;


        public GetTrainingMaterialsQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetTrainingMaterialsDto> Handle(GetTrainingMaterialsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.TrainingMaterials
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
                .GetPagedAsync<Domain.Entities.TrainingMaterial, TrainingMaterialData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetTrainingMaterialsDto()
            {
                Success = true,
                Message = "Training Materials are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }
    }
}
