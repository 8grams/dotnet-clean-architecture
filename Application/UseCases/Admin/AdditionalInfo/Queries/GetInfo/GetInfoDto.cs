using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Queries.GetInfo
{
    public class GetInfoDto : BaseDto
    {
        public InfoData Data { set; get; }
    }
}
