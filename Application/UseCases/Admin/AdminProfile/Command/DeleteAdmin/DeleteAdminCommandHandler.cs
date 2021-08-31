using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.DeleteAdmin
{
    public class DeleteAdminCommandHandler : IRequestHandler<DeleteAdminCommand, DeleteAdminDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteAdminCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteAdminDto> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var admin = await _context.Admins.FindAsync(id);
                if (admin != null) _context.Admins.Remove(admin);
            }

            await _context.SaveChangesAsync(cancellationToken);    
            
            return new DeleteAdminDto
            {
                Success = true,
                Message = "Admin has been successfully deleted"
            };
        }
    }


}