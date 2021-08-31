using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.FAQ.Models;
using System.Linq.Dynamic.Core;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Queries.GetFAQs
{
    public class GetFAQsQueryHandler : IRequestHandler<GetFAQsQuery, GetFAQsDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;


        public GetFAQsQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetFAQsDto> Handle(GetFAQsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.Faqs.AsQueryable();
            
            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Answer.Contains(@0) || Question.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.Faq, FAQData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetFAQsDto()
            {
                Success = true,
                Message = "FAQs are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };

        }
    }
}
