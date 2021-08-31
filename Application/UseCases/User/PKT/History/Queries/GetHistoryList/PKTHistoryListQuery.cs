using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.PKT.History.Queries.GetHistoryList
{
    public class PKTHistoryListQuery : PaginationQuery, IRequest<PKTHistoryListDto>
    {
        public string SalesCode { set; get; }
    }
}
