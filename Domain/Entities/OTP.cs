using System;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class OTP : BaseEntity
    {
        public int UserId { set; get; }
        public string Pin { set; get; }
        public DateTime ExpiresAt { set; get; }

        public virtual User User { set; get; }
    }
}
