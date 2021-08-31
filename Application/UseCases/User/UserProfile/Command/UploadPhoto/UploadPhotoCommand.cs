using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UploadPhoto
{
    public class UploadPhotoCommand : BaseQueryCommand, IRequest<UploadPhotoDto>
    {
        public UploadPhotoCommandData Data { set; get; }
    }

    public class UploadPhotoCommandData
    {
        public string FileByte { set; get; }
    }
}
