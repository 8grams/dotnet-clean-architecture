using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Queries.GetMasterCars
{
    public class GetMasterCarsQuery : AdminPaginationQuery, IRequest<GetMasterCarsDto>
    {
    }
}
