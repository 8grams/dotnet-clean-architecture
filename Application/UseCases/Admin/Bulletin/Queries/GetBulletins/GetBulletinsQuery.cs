using MediatR;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Queries.GetBulletins
{
    public class GetBulletinsQuery : AdminPaginationQuery, IRequest<GetBulletinsDto>
    {
    }
}
