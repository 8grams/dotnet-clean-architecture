using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public Menu()
        {
            Permissions = new HashSet<Permission>();
        }

        public string Name { set; get; }
        public string Description { set; get; }
        public int MenuTypeId { set; get; }

        public virtual MenuType MenuType { set; get; }
        public virtual ICollection<Permission> Permissions { get; private set; }
    }
}
