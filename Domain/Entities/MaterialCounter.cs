using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class MaterialCounter : BaseEntity
    {
        public int UserId { set; get; }
        public string ContentType { set; get; }
        public int ContentId { set; get; }
    }
}
