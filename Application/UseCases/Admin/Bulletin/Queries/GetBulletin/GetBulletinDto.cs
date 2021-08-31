using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.Bulletin.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Queries.GetBulletin
{
    public class GetBulletinDto : BaseDto
    {
        public BulletinData Data { set; get; }
    }
}
