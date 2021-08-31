using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Queries.CheckNotification
{
    public class CheckNotificationDto : BaseDto
    {
        public CheckNotificationData Data { set; get; }
    }

    public class CheckNotificationData
    {
        public bool HasNewGuide { set; get; }
        public bool HasNewTraining { set; get; }
        public bool HasNewBulletin { set; get; }
        public bool HasNewInfo { set; get; }
        public bool HasNewNotification { set; get; }
    }
}
