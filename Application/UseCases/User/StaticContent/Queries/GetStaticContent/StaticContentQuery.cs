using System;
using MediatR;

namespace SFIDWebAPI.Application.UseCases.User.StaticContent.Queries.GetStaticContent
{
    public class StaticContentQuery : IRequest<StaticContentDto>
    {
        public string Name { set; get; }
    }
}
