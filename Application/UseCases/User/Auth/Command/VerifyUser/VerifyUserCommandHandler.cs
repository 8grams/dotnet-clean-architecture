using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Auth.Command.VerifyUser
{
    public class VerifyUserCommandHandler : IRequestHandler<VerifyUserCommand, VerifyUserDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public VerifyUserCommandHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VerifyUserDto> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(e => e.SalesmanData)
                .Where(e => e.UserName.Equals(request.Data.SalesCode))
                .Where(e => e.IsActive == true)
                .FirstOrDefaultAsync();

            var response = new VerifyUserDto();
            if (user != null)
            {
                user.IsRegistered = true;

                response.Success = true;
                response.Message = "User verified";
                response.Origin = "verify_user.success.default";
                response.Data = _mapper.Map<ProfileDto>(user);
            }
            else
            {
                response.Success = false;
                response.Message = "Kode sales tidak valid";
                response.Origin = "verify_user.fail.invalid_sales_code";
            }

            return response;
        }
    }
}
