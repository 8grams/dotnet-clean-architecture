using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.User.Training.Category.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.User.Training.Category.Queries.GetTrainingCategoryList
{
    public class TrainingCategoryListQueryHandler : IRequestHandler<TrainingCategoryListQuery, TrainingCategoryListDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public TrainingCategoryListQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TrainingCategoryListDto> Handle(TrainingCategoryListQuery request, CancellationToken cancellationToken)
        {
            var data = _context.MasterCars
                .Include(e => e.ImageCover)
                .Include(e => e.ImageThumbnail)
                .Include(e => e.ImageLogo)
                .Where(e => e.TrainingActive == true);

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.MasterCar, TrainingCategoryData>(request.PagingPage, request.PagingLimit, _mapper);

            return new TrainingCategoryListDto()
            {
                Success = true,
                Message = "Training Categories are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }
    }
}
