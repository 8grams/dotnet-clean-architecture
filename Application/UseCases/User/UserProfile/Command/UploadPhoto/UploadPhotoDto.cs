using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UploadPhoto
{
    public class UploadPhotoDto : BaseDto
    {
        public ProfileDto Data { set; get; }
    }
}
