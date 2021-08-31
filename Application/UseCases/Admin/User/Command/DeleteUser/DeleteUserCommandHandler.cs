using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteUserCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteUserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                // get user first
                var user = await _context.Users.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (user == null) continue;
                
                // if meta found, delete salesmen meta as well
                var meta = await _context.SalesmenMeta.Where(e => e.SalesmanCode == user.UserName).ToListAsync();
                if (meta != null)
                {
                    _context.SalesmenMeta.RemoveRange(meta);
                }

                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeleteUserDto
            {
                Success = true,
                Message = "User has been successfully deleted"
            };
        }
    }
}