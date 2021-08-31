using System;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class AccessToken : BaseEntity
    {
        public string AuthToken { set; get; }
        public string Type { set; get; }
        public int UserId { set; get; }
        public DateTime ExpiresAt { set; get; }

        public virtual User User { set; get; }
    }
}
