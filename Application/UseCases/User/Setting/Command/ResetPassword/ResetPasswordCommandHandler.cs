using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Infrastructure.Persistences;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordDto>
    {
        private readonly ISFDDBContext _context;

        public ResetPasswordCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<ResetPasswordDto> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);

            var response = new ResetPasswordDto();
            if (user == null || !user.Password.Equals(DBUtil.PasswordHash(request.Data.OldPassword)))
            {
                response.Success = false;
                response.Message = "Old password does not match";
                response.Origin = "reset_password.fail.missmatch_old_password";
            }
            else
            {
                // change user password
                user.Password = DBUtil.PasswordHash(request.Data.NewPassword);
                await _context.SaveChangesAsync(cancellationToken);
                
                response.Success = true;
                response.Message = "Password is successfully changed";
                response.Origin = "reset_password.success.default";
            }
            return response;
        }
    }
}
