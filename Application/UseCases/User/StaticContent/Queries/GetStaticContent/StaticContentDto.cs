using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.StaticContent.Queries.GetStaticContent
{
    public class StaticContentDto : BaseDto
    {
        public StaticContentData Data { set; get; }
    }

    public class StaticContentData
    {
        public string Content { set; get; }
    }
}
