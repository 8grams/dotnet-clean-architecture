using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Infrastructure.Persistences;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.ForgotPassword
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;

        public ForgotPasswordHandler(ISFDDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ForgotPasswordDto> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = new Domain.Entities.User();
            if (request.Data.Type.Equals("phone"))
            {
                user = await _context.Users
                    .Where(e => e.UserName.Equals(request.Data.SalesCode))
                    .Where(e => e.Phone.Equals(request.Data.Identifier))
                    .FirstOrDefaultAsync();
            }
            else
            {
                user = await _context.Users
                    .Where(e => e.UserName.Equals(request.Data.SalesCode))
                    .Where(e => e.Email.Equals(request.Data.Identifier))
                    .FirstOrDefaultAsync();
            }

            var response = new ForgotPasswordDto();
            if (user == null)
            {
                response.Success = false;
                response.Message = "Email/No Telp anda tidak terdaftar";
                response.Origin = request.Data.Type.Equals("phone") ? "forgot_password.fail.phone_not_registered" : "forgot_password.fail_email.not_registered";
                return response;
            }

            // generate new password
            var newPassword = DBUtil.RandomString(6);
            user.Password = DBUtil.PasswordHash(newPassword);
            user.RawPassword = newPassword;
            await _context.SaveChangesAsync(cancellationToken);

            response.Success = true;
            response.Message = "Forgot password has sent to your ∂email";
            response.Origin = "forgot_password.success.default";

            // publish after forgot password
            await _mediator.Publish(new AfterForgotPassword { User = user }, cancellationToken);


            return response;
        }
    }
}
