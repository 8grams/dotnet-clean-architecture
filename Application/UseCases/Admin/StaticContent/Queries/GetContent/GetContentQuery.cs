using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.StaticContent.Queries.GetContent
{
    public class GetContentQuery : BaseAdminQueryCommand, IRequest<GetContentDto>
    {
        public string Name { set; get; }
    }
}
