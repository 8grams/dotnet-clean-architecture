using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.User.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;


        public GetUserQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FindAsync(request.Id);

            return new GetUserDto()
            {
                Success = true,
                Message = "User are succefully retrieved",
                Data = _mapper.Map<UserData>(user.SalesmanMeta)
            };
        }
    }
}
