using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Command.DeleteInfo
{
    public class DeleteInfoCommandHandler : IRequestHandler<DeleteInfoCommand, DeleteInfoDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteInfoCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteInfoDto> Handle(DeleteInfoCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var info = await _context.AdditionalInfos.FindAsync(id);
                if (info != null) _context.AdditionalInfos.Remove(info);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeleteInfoDto
            {
                Success = true,
                Message = "Info has been successfully deleted"
            };
        }
    }


}