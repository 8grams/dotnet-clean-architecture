using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Queries.GetProfile
{
    public class GetProfileDto : BaseDto
    {
        public ProfileDto Data { set; get; }
    }
}
