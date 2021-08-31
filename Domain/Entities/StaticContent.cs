using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class StaticContent : BaseEntity
    {
        public const string TYPE_APP_INFO = "appinfo";
        public const string TYPE_DISCLAIMER = "disclaimer";
        public const string TYPE_TERM_CONDITION = "term";
        public const string TYPE_PRIVACY_POLICY = "privacy";
        public const string TYPE_TUTORIAL = "tutorial";


        public string AppInfo { set; get; }
        public string Disclaimer { set; get; }
        public string TermCondition { set; get; }
        public string PrivacyPolicy { set; get; }
        public string Tutorial { set; get; }
    }
}
