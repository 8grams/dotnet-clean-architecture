namespace WebApi.Application.Models.Notifications
{
    public class FCMMessage
    {
        public const string TYPE_TOPIC = "topic";
        public const string TYPE_SINGLE = "single";

        public const string DATA_MESSAGE = "data";
        public const string NOTIFICATION_MESSAGE = "message";

        public string Title { set; get; }
        public string Body { set; get; }
        public string Type { set; get; }
        public string Key { set; get; }
        public string NotificationType { set; get; }
        public string NotificationKey { set; get; }
    }
}
