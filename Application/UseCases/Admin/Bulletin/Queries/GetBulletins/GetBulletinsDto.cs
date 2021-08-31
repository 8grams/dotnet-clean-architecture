using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.Bulletin.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Queries.GetBulletins
{
    public class GetBulletinsDto : PaginationDto
    {
        public IList<BulletinData> Data { set; get; }
    }
}
