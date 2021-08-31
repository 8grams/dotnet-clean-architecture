using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.UseCases.User.StaticContent.Queries.GetStaticContent
{
    public class StaticContentQueryHandler : IRequestHandler<StaticContentQuery, StaticContentDto>
    {
        private readonly ISFDDBContext _context;
        private Utils _utils;

        public StaticContentQueryHandler(ISFDDBContext context, Utils utils)
        {
            _context = context;
            _utils = utils;
        }

        public async Task<StaticContentDto> Handle(StaticContentQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.StaticContent.FirstAsync();
            var result = request.Name switch
            {
                "appinfo" => data.AppInfo,
                "disclaimer" => data.Disclaimer,
                "privacy" => data.PrivacyPolicy,
                "term" => data.TermCondition,
                "tutorial" => _utils.GetValidUrl(data.Tutorial),
                _ => data.Disclaimer,
            };
            return new StaticContentDto()
            {
                Success = true,
                Message = "Content are succefully retrieved",
                Data = new StaticContentData
                {
                    Content = result
                }
            };
        }
    }
}
