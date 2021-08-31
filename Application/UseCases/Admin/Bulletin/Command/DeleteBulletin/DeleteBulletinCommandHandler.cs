using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Command.DeleteBulletin
{
    public class DeleteBulletinCommandHandler : IRequestHandler<DeleteBulletinCommand, DeleteBulletinDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteBulletinCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteBulletinDto> Handle(DeleteBulletinCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var bulletin = await _context.Bulletins.FindAsync(id);
                if (bulletin != null) _context.Bulletins.Remove(bulletin);
            }

            await _context.SaveChangesAsync(cancellationToken);    
            
            return new DeleteBulletinDto
            {
                Success = true,
                Message = "Bulletin has been successfully deleted"
            };
        }
    }


}