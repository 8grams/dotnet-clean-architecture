using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Infrastructure.Persistences;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.VerifyUser
{
    public class VerifyCommandHandler : IRequestHandler<VerifyUserCommand, VerifyUserDto>
    {
        private readonly ISFDDBContext _context;
         private readonly IMediator _mediator;

        public VerifyCommandHandler(ISFDDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<VerifyUserDto> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(e => e.Id == request.UserId)
                .Where(e => e.Password == DBUtil.PasswordHash(request.Data.Password))
                .FirstOrDefaultAsync();

            var response = new VerifyUserDto();
            if (user == null)
            {
                response.Success = false;
                response.Message = "Password Salah";
                response.Origin = "verify_user.fail.default";
            }
            else
            {
                response.Success = true;
                response.Message = "User is verified";
                response.Origin = "verify_user.success.default";
            }

            return response;
        }
    }
}
