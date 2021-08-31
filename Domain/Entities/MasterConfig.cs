using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class MasterConfig : BaseEntity
    {
        public MasterConfig()
        {
            Roles = new HashSet<Role>();
            Users = new HashSet<User>();
        }

        public string Category { set; get; }
        public string ValueId { set; get; }
        public string ValueCode { set; get; }
        public string ValueDesc { set; get; }
        public int Sequence { set; get; }

        public virtual ICollection<Role> Roles { get; private set; }
        public virtual ICollection<User> Users { get; private set; }
    }
}
