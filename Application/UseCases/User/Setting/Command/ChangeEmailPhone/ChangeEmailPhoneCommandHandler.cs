using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Setting.Command.ChangeEmailPhone
{
    public class VerifyCommandHandler : IRequestHandler<ChangeEmailPhoneCommand, ChangeEmailPhoneDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public VerifyCommandHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ChangeEmailPhoneDto> Handle(ChangeEmailPhoneCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(e => e.Id == request.UserId)
                .FirstAsync();

            var msg = "Your Email is successfully updated";
            var origin = "update.success.email_updated";
            if (request.Data.Type == "email")
            {
                user.Email = request.Data.Identifier;
            }
            else 
            {
                user.Phone = request.Data.Identifier;
                msg = "Your Phone is successfully updated";
                origin = "update.success.phone_updated";
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            var response = new ChangeEmailPhoneDto();
            if (user == null)
            {
                response.Success = false;
                response.Message = "Failed to update";
                response.Origin = "update.fail.default";
            }
            else
            {
                response.Success = true;
                response.Message = msg;
                response.Origin = origin;
                response.Data = _mapper.Map<ProfileDto>(user);
            }

            return response;
        }
    }
}
