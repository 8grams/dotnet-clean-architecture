using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.User.Auth.Event;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.OTP.Trigger
{
    public class OTPTriggerCommandHandler : IRequestHandler<OTPTriggerCommand, BaseDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMediator _mediator;

        public OTPTriggerCommandHandler(ISFDDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<BaseDto> Handle(OTPTriggerCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return new OTPTriggerDto()
                {
                    Success = false,
                    Message = "Invalid User"
                };
            }

            // create otp
            await _mediator.Publish(new SendOTP
            {
                User = user
            }, cancellationToken);

            return new OTPTriggerDto()
            {
                Success = true,
                Message = "OTP sent"
            };
        }
    }
}
