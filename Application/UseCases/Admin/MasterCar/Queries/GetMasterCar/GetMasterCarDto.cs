using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.MasterCar.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Queries.GetMasterCar
{
    public class GetMasterCarDto : PaginationDto
    {
        public MasterCarData Data { set; get; }
    }   
}
