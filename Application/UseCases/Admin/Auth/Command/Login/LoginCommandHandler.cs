using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Infrastructure.Persistences;

namespace SFIDWebAPI.Application.UseCases.Admin.Auth.Command.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public LoginCommandHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var admin = await _context.Admins
                .Where(e => e.Email.Equals(request.Data.Email))
                .Where(e => e.Password.Equals(DBUtil.PasswordHash(request.Data.Password)))
                .FirstOrDefaultAsync();

            var response = new LoginDto();
            if (admin == null)
            {
                response.Success = false;
                response.Message = "Login Failed";
            }
            else
            {
                // create access token
                var token = new Domain.Entities.AdminToken {
                    AdminId = admin.Id,
                    Admin = admin,
                    AuthToken = DBUtil.GenerateAuthToken(),
                    Type = "Bearer",
                    ExpiresAt = DateTime.Now.AddDays(7)
                };
                _context.AdminTokens.Add(token);

                admin.LastLogin = DateTime.Now;
                _context.Admins.Update(admin);

                await _context.SaveChangesAsync(cancellationToken);

                response.Success = true;
                response.Message = "Login Success";
                response.Data = _mapper.Map<TokenData>(token);
            }

            return response;
        }
    }
}