using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Bulletin.Queries.GetBulletinList
{
    public class BulletinListQuery : PaginationQuery, IRequest<BulletinListDto>
    {
        public IList<FilterParams> Filters { set; get; }
    }
}
