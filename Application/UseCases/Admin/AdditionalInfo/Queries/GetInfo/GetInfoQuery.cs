using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Queries.GetInfo
{
    public class GetInfoQuery : BaseAdminQueryCommand, IRequest<GetInfoDto>
    {
        public int Id { set; get; }
    }
}
