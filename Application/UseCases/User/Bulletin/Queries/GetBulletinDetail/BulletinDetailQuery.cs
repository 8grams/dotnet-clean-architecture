using System;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Bulletin.Queries.GetBulletinDetail
{
    public class BulletinDetailQuery : BaseQueryCommand, IRequest<BulletinDetailDto>
    {
        public int Id { set; get; }
    }
}
