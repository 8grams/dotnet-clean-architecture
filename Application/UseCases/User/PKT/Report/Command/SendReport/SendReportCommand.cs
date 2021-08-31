using System;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.SendReport
{
    public class SendReportCommand : BaseQueryCommand, IRequest<SendReportDto>
    {
        public VerifyVinCommandData Data { set; get; } 
    }

    public class VerifyVinCommandData
    {
        public string Vin { set; get; }
        public string Image { set; get; }
        public string Type { set; get; }
        public string SalesCode { set; get; }
        public int? Status { set; get; }
    }
}
