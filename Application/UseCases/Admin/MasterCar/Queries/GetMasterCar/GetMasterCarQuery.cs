using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Queries.GetMasterCar
{
    public class GetMasterCarQuery : BaseAdminQueryCommand, IRequest<GetMasterCarDto>
    {
        public int Id { set; get; }
    }
}
