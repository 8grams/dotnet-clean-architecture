using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Queries.GetInfos
{
    public class GetInfosDto : PaginationDto
    {
        public IList<InfoData> Data { set; get; }
    }
}
