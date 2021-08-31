using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.VerifyVin
{
    public class VerifyVinCommand : BaseQueryCommand, IRequest<VerifyVinDto>
    {
        public VerifyVinCommandData Data { set; get; } 
    }

    public class VerifyVinCommandData
    {
        public string SalesCode { set; get; }
        public string Vin { set; get; }
        public string Type { set; get; }
    }
}
