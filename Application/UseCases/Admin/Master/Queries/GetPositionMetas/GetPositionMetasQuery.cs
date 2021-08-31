using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetPositionMetas
{
    public class GetPositionMetasQuery : AdminPaginationQuery, IRequest<GetPositionMetasDto>
    {
    }
}
