using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.DeleteReport
{
    public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, DeleteReportDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public DeleteReportCommandHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DeleteReportDto> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            var pktHistory = await _context.PKTHistories.FindAsync(request.Data.Id);
            _context.PKTHistories.Remove(pktHistory);
            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeleteReportDto()
            {
                Success = true,
                Message = "Report is successfully deleted"
            };
        }
    }
}
