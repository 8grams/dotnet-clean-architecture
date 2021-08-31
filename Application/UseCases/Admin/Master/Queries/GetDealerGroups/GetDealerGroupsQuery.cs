using MediatR;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealerGroups
{
    public class GetDealerGroupsQuery : IRequest<GetDealerGroupsDto>
    {
        public string QuerySearch { set; get; }
    }
}
