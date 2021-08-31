using System.Collections.Generic;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealers
{
    public class GetDealersQuery : IRequest<GetDealersDto>
    {
        public string QuerySearch { set; get; }
        public IList<FilterParams> Filters { set; get; }
    }
}
