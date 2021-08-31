using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Bulletin.Command.UpdateBulletin
{
    public class UpdateBulletinCommand : BaseAdminQueryCommand, IRequest<UpdateBulletinDto>
    {
        public UpdateBulletinData Data { set; get; }
    }

    public class UpdateBulletinData
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public int ImageThumbnailId { set; get; }
        public string FileCode { set; get; }
        public string FileByte { set; get; }
        public string FileName { set; get; }
        public bool IsRecommended { set; get; }
        public bool IsActive  { set; get; }
    }
}
