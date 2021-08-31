using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Command.UpdatePositionMeta
{
    public class UpdatePositionMetaCommandHandler : IRequestHandler<UpdatePositionMetaCommand, UpdatePositionMetaDto>
    {
        private readonly ISFDDBContext _context;

        public UpdatePositionMetaCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<UpdatePositionMetaDto> Handle(UpdatePositionMetaCommand request, CancellationToken cancellationToken)
        {
            var position = await _context.PositionMetas.FindAsync(request.Data.Id);
            position.Code = request.Data.Code;
            position.Description = request.Data.Description;

            _context.PositionMetas.Update(position);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdatePositionMetaDto
            {
                Success = true,
                Message = "Job Position has been successfully created"
            };
        }
    }
}