using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Queries.GetProfile
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, GetProfileDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetProfileQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(e => e.Id == request.Id)
                .Include(e => e.SalesmanData)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return new GetProfileDto
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            return new GetProfileDto()
            {
                Success = true,
                Message = "User is succefully retrieved",
                Data = _mapper.Map<ProfileDto>(user)
            };
        }
    }
}
