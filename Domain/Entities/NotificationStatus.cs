using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class NotificationStatus : BaseEntity
    {
        public int NotificationId { set; get; }
        public int UserId { set; get; }
        public bool IsDeleted { set; get; }
        public bool HasRead { set; get; }

        public virtual Notification Notification { get; set; }
    }
}
