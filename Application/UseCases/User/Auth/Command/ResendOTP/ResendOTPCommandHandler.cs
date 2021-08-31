using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.User.Auth.Event;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.ResendOTP
{
    public class ResendOTPCommandHandler : IRequestHandler<ResendOTPCommand, ResendOTPDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;

        public ResendOTPCommandHandler(ISFDDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<ResendOTPDto> Handle(ResendOTPCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Data.UserId);
            if (request.Data.Concern.Equals("register"))
            {
                // get last otp, update expires date, and resend
                var otp = await _context.OTPs
                    .Where(e => e.UserId == user.Id)
                    .OrderByDescending(e => e.CreateDate)
                    .FirstOrDefaultAsync();

                if (otp != null)
                {
                    otp.ExpiresAt = DateTime.Now.AddMinutes(-10);
                    _context.OTPs.Update(otp);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            await _mediator.Publish(new SendOTP
            {
                User = user
            }, cancellationToken);

            return new ResendOTPDto()
            {
                Success = true,
                Message = "OTP is resent",
                Origin = "resend_otp.success.default"
            };
        }
    }
}
