using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.FAQ.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Queries.GetFAQ
{
    public class GetFAQQueryHandler : IRequestHandler<GetFAQQuery, GetFAQDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetFAQQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetFAQDto> Handle(GetFAQQuery request, CancellationToken cancellationToken)
        {
            var faq = await _context.Faqs.FindAsync(request.Id);
            var response = new GetFAQDto()
            {
                Success = true,
                Message = "FAQ is succefully retrieved"
            };

            if (faq != null)
            {
                response.Data = _mapper.Map<FAQData>(faq);
            }
            else
            {
                response.Success = false;
                response.Message = "FAQ is not found";
            }

            return response;
        }
    }
}
