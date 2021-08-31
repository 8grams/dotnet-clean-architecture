using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileDto>
    {
        private readonly ISFDDBContext _context;

        public UpdateProfileCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<UpdateProfileDto> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            user.Phone = request.Data.Phone;
            user.Email = request.Data.Email;
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateProfileDto()
            {
                Success = true,
                Message = "Profile is successfully updated"
            };

        }
    }
}
