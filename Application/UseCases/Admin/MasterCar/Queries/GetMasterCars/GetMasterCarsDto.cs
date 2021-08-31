using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.MasterCar.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Queries.GetMasterCars
{
    public class GetMasterCarsDto : PaginationDto
    {
        public IList<MasterCarData> Data { set; get; }
    }   
}
