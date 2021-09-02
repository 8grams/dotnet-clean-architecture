using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebApi.Application.Interfaces;

namespace WebApi.Application.UseCases.User.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserDto>
    {
        private readonly IWebApiDbContext _context;

        public DeleteUserCommandHandler(IWebApiDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteUserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var user = await _context.Users.FindAsync(id);
                if (user != null) _context.Users.Remove(user);
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