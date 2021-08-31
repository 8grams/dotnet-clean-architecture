using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.User.Analytics.Command.CreateUserPresence
{
    public class CreateUserPresenceCommandHandler : IRequestHandler<CreateUserPresenceCommand, CreateUserPresenceDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public CreateUserPresenceCommandHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateUserPresenceDto> Handle(CreateUserPresenceCommand request, CancellationToken cancellationToken)
        {
            _context.UserPresences.Add(new Domain.Entities.UserPresence {
                Uuid = Guid.NewGuid().ToString(),
                UserId = request.Data.UserId,
                DealerId = request.Data.DealerId,
                DealerBranchId = request.Data.DealerBranchId,
                JobPositionId = request.Data.JobPositionId,
                CityId = request.Data.CityId,
                Platform = request.Data.Platform,
                CreateDate = DateTime.Now,
                IsNew = request.Data.IsNew
            });
            
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateUserPresenceDto()
            {
                Success = true,
                Message = "User presensces updated"
            };
        }
    }
}
