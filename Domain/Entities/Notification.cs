using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string OwnerId { set; get; }
        public string OwnerType { set; get; }
        public string Title { set; get; }
        public string Content { set; get; }
        public string Attachment { set; get; }
        public bool IsDeletable { set; get; }

        public virtual NotificationStatus NotificationStatus { get; set; }
    }
}
