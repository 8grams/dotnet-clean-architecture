using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UpdateProfile
{
    public class UpdateProfileCommand : BaseQueryCommand, IRequest<UpdateProfileDto>
    {
        public UpdateProfileCommandData Data { set; get; }
    }

    public class UpdateProfileCommandData
    {
        public string Phone { set; get; }
        public string Email { set; get; }
    }
}
