using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.UseCases.Admin.StaticContent.Queries.GetContent
{
    public class GetContentQueryHandler : IRequestHandler<GetContentQuery, GetContentDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;
        private readonly Utils _utils;


        public GetContentQueryHandler(ISFDDBContext context, IMapper mapper, Utils utils)
        {
            _context = context;
            _mapper = mapper;
            _utils = utils;
        }

        public async Task<GetContentDto> Handle(GetContentQuery request, CancellationToken cancellationToken)
        {
            var staticContent = await _context.StaticContent.FirstAsync();
            var content = "";
            switch (request.Name)
            {
                case Domain.Entities.StaticContent.TYPE_APP_INFO:
                    content = staticContent.AppInfo;
                    break;
                case Domain.Entities.StaticContent.TYPE_DISCLAIMER:
                    content = staticContent.Disclaimer;
                    break;
                case Domain.Entities.StaticContent.TYPE_PRIVACY_POLICY:
                    content = staticContent.PrivacyPolicy;
                    break;
                case Domain.Entities.StaticContent.TYPE_TERM_CONDITION:
                    content = staticContent.TermCondition;
                    break;
                case Domain.Entities.StaticContent.TYPE_TUTORIAL:
                    content = _utils.GetValidUrl(staticContent.Tutorial);
                    break;
                default:
                    throw new InvalidOperationException("Cannot process");
            }

            return new GetContentDto()
            {
                Success = true,
                Message = "Static content is successfully retrieved",
                Data = new ContentData
                {
                    Name = request.Name,
                    Content = content
                }
            };
        }
    }
}
