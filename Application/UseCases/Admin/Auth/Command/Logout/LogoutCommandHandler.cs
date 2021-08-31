using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Auth.Command.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, LogoutDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public LogoutCommandHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LogoutDto> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var token = await _context.AdminTokens
                .Where(e => e.AuthToken.Equals(request.AuthToken))
                .FirstOrDefaultAsync();

            if (token != null) 
            {
                _context.AdminTokens.Remove(token);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return new LogoutDto()
            {
                Success = true,
                Message = "Logout success"
            };
        }
    }
}