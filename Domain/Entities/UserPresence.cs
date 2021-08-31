using System;

namespace SFIDWebAPI.Domain.Entities
{
    public class UserPresence
    {
        public string Uuid { set; get; }
        public int UserId { set; get; }
        public int JobPositionId { set; get; }
        public int DealerId { set; get; }
        public int CityId { set; get; }
        public int DealerBranchId { set; get; }
        public string Platform { set; get; }
        public DateTime CreateDate { set; get; }
        public bool IsNew { set; get; }
        public virtual User User { set; get; }
    }
}
