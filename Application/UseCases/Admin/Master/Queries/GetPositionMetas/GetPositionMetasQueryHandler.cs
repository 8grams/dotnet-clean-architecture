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

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetPositionMetas
{
    public class GetPositionMetasQueryHandler : IRequestHandler<GetPositionMetasQuery, GetPositionMetasDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetPositionMetasQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetPositionMetasDto> Handle(GetPositionMetasQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data =  _context.PositionMetas
                .Select(pos => new PositionData() {
                    Id = pos.Id,
                    Code = pos.Code,
                    Description = pos.Description,
                    CreateBy = pos.CreateBy,
                    CreateDate = pos.CreateDate
                });

            var results = await data.ProjectTo<PositionMetaData>(_mapper.ConfigurationProvider).ToListAsync();

            return new GetPositionMetasDto()
            {
                Success = true,
                Message = "Job Positions are succefully retrieved",
                Data = results
            };
        }
    }
}
