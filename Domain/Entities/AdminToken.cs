using System;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class AdminToken : BaseEntity
    {
        public string AuthToken { set; get; }
        public string Type { set; get; }
        public int AdminId { set; get; }
        public DateTime ExpiresAt { set; get; }
        public virtual Admin Admin { set; get; }
    }
}
