using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UpdateFirebaseToken
{
    public class UpdateFirebaseTokenCommand : BaseQueryCommand, IRequest<UpdateFirebaseTokenDto>
    {
        public UpdateFirebaseTokenCommandData Data { set; get; }
    }

    public class UpdateFirebaseTokenCommandData
    {
        public string Token { set; get; }
    }
}
