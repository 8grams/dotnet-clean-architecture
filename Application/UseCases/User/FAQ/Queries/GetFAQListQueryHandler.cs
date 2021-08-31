using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.User.FAQ.Models;

namespace SFIDWebAPI.Application.UseCases.User.FAQ.Queries.GetFAQList
{
    public class GetFAQListQueryHandler : IRequestHandler<GetFAQListQuery, GetFAQListDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetFAQListQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetFAQListDto> Handle(GetFAQListQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = await _context.Faqs
                .Where(e => e.IsActive == true)
                .ToListAsync();

            return new GetFAQListDto()
            {
                Success = true,
                Message = "FAQ are succefully retrieved",
                Data = _mapper.Map<IList<FAQData>>(data)
            };
        }
    }
}
