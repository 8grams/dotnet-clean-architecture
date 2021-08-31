using SFIDWebAPI.Application.UseCases.User.Bulletin.Models;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Bulletin.Queries.GetBulletinDetail
{
    public class BulletinDetailDto : BaseDto
    {
        public BulletinData Data { set; get; }
    }
}
