using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Command.DeletePositionMeta
{
    public class DeletePositionMetaCommandHandler : IRequestHandler<DeletePositionMetaCommand, DeletePositionMetaDto>
    {
        private readonly ISFDDBContext _context;

        public DeletePositionMetaCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeletePositionMetaDto> Handle(DeletePositionMetaCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var position = await _context.PositionMetas.FindAsync(id);
                if (position != null) _context.PositionMetas.Remove(position);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeletePositionMetaDto
            {
                Success = true,
                Message = "Job Position has been successfully deleted"
            };
        }
    }
}