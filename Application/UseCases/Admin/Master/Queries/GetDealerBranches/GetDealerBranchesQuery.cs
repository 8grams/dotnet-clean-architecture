using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealerBranches
{
    public class GetDealerBranchesQuery : IRequest<GetDealerBranchesDto>
    {
        public string QuerySearch { set; get; }
        public IList<FilterParams> Filters { set; get; }
    }
}
