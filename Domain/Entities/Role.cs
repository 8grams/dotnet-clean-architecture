using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            Permissions = new HashSet<Permission>();
            Admins = new HashSet<Admin>();
        }

        public string Name { set; get; }
        public string Description { set; get; }
        public int MasterConfigId { set; get; }
        
        public virtual ICollection<Permission> Permissions { get; private set; }
        public virtual ICollection<Admin> Admins { get; private set; }
        public virtual MasterConfig MasterConfig { set; get; }
    }
}
