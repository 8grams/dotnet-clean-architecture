using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Command.CreateBulletin
{
    public class CreateBulletinCommand : BaseAdminQueryCommand, IRequest<CreateBulletinDto>
    {
        public CreateBulletinData Data { set; get; }
    }

    public class CreateBulletinData
    {
        public string Title { set; get; }
        public int ImageThumbnailId { set; get; }
        public string FileCode { set; get; }
        public string FileByte { set; get; }
        public string FileName { set; get; }
        public bool IsRecommended { set; get; }
        public bool IsActive  { set; get; }
    }
}
