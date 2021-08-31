using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Command.CreatePositionMeta
{
    public class CreatePositionMetaCommandHandler : IRequestHandler<CreatePositionMetaCommand, CreatePositionMetaDto>
    {
        private readonly ISFDDBContext _context;

        public CreatePositionMetaCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<CreatePositionMetaDto> Handle(CreatePositionMetaCommand request, CancellationToken cancellationToken)
        {
            var position = new Domain.Entities.PositionMeta
            {
                Code = request.Data.Code,
                Description = request.Data.Description
            };

            _context.PositionMetas.Add(position);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreatePositionMetaDto
            {
                Success = true,
                Message = "Job Position has been successfully created"
            };
        }
    }
}