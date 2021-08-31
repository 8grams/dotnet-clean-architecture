using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class MaterialStatus : BaseEntity
    {
        public int UserId { set; get; }
        public int NewBulletin { set; get; }
        public int NewInfo { set; get; }
        public int NewTraining { set; get; }
        public int NewGuide { set; get; }

        public virtual User User { set; get; }
    }
}
