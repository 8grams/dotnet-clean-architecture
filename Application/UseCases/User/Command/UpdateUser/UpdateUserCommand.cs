using MediatR;
using WebApi.Application.Models.Query;

namespace WebApi.Application.UseCases.User.Command.UpdateUser
{
    public class UpdateUserCommand : BaseQueryCommand, IRequest<UpdateUserDto>
    {
        public UpdateUserData Data { set; get; }
    }

    public class UpdateUserData
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public int ImageThumbnailId { set; get; }
        public string FileCode { set; get; }
        public string FileByte { set; get; }
        public string FileName { set; get; }
        public bool IsRecommended { set; get; }
        public bool IsActive  { set; get; }
        public bool IsDownloadable  { set; get; }
    }
}
