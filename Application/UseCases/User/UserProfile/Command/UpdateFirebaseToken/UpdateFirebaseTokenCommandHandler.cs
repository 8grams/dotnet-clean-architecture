using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UpdateFirebaseToken
{
    public class UpdateFirebaseTokenCommandHandler : IRequestHandler<UpdateFirebaseTokenCommand, UpdateFirebaseTokenDto>
    {
        private readonly ISFDDBContext _context;

        public UpdateFirebaseTokenCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<UpdateFirebaseTokenDto> Handle(UpdateFirebaseTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            user.FirebaseToken = request.Data.Token;
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateFirebaseTokenDto()
            {
                Success = true,
                Message = "Token is successfully saved"
            };

        }
    }
}
