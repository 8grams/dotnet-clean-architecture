using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.User.Bulletin.Models;

namespace SFIDWebAPI.Application.UseCases.User.Bulletin.Queries.GetBulletinList
{
    public class BulletinListDto : PaginationDto
    {
        public IList<BulletinData> Data { set; get; }
        public IList<int> AvailableYears { set; get; }
    }
}
