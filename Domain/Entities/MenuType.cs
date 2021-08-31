using System;
using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class MenuType : BaseEntity
    {
        public MenuType()
        {
            Menus = new HashSet<Menu>();
        }

        public string Name { set; get; }
        public string Description { set; get; }

        public virtual ICollection<Menu> Menus { get; private set; }
    }
}
