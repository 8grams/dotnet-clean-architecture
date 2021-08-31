using System;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class Recommendation
    {
        public const string TYPE_BULLETIN = "bulletin";
        public const string TYPE_INFO = "info";
        public const string TYPE_TRAINING = "training";
        public const string TYPE_GUIDE = "guide";

        public string ContentType { set; get; }
        public int ContentId { set; get; }
        public DateTime PublishedAt { set; get; }
        public DateTime ExpiresAt { set; get; }
        public string CreateBy { set; get; }
        public DateTime? CreateDate { set; get; }
        public string LastUpdateBy { set; get; }
        public DateTime? LastUpdateDate { set; get; }
        public Int16 RowStatus { set; get; }
    }
}
