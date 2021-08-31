using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Queries.GetBulletin
{
    public class GetBulletinQuery : BaseAdminQueryCommand, IRequest<GetBulletinDto>
    {
        public int Id { set; get; }
    }
}
