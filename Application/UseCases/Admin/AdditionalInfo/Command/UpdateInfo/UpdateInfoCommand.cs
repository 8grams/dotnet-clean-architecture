using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Command.UpdateInfo
{
    public class UpdateInfoCommand : BaseAdminQueryCommand, IRequest<UpdateInfoDto>
    {
        public UpdateInfoData Data { set; get; }
    }

    public class UpdateInfoData
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
