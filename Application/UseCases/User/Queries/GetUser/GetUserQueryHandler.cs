using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.Interfaces;
using WebApi.Application.UseCases.User.Models;

namespace WebApi.Application.UseCases.User.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserDto>
    {
        private readonly IWebApiDbContext _context;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IWebApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(e => e.Id == request.Id)
                .FirstAsync();
            var response = new GetUserDto()
            {
                Success = true,
                Message = "User is successfully retrieved"
            };

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }  

            return new GetUserDto()
            {
                Success = true,
                Message = "User is successfully retrieved",
                Data = _mapper.Map<UserData>(user)
            };
        }
    }
}
