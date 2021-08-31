using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class Permission : BaseEntity
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public int? RoleId { set; get; }
        public int? MenuId { set; get; }

        public virtual Role Role { set; get; }
        public virtual Menu Menu { get; set; }
    }
}
