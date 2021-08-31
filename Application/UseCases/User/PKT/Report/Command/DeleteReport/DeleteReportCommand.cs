using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.DeleteReport
{
    public class DeleteReportCommand : IRequest<DeleteReportDto>
    {
        public DeleteReportCommandData Data { set; get; } 
    }

    public class DeleteReportCommandData
    {
        public int Id { set; get; }
    }
}