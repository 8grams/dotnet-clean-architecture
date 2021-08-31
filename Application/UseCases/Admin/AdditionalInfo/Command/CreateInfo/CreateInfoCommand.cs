using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Command.CreateInfo
{
    public class CreateInfoCommand : BaseAdminQueryCommand, IRequest<CreateInfoDto>
    {
        public CreateInfoData Data { set; get; }
    }

    public class CreateInfoData
    {
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
