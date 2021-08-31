using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.StaticContent.Queries.GetContent
{
    public class GetContentDto : BaseDto
    {
        public ContentData Data { set; get; }
    }

    public class ContentData : BaseDtoData
    {
        public string Name { set; get; }
        public string Content { set; get; }
    }
}
