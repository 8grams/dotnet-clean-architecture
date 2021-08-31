using System;
using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class Admin : BaseEntity
    {
        public Admin()
        {
            AdminTokens = new HashSet<AdminToken>();
        }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Password { set; get; }
        public DateTime? LastLogin { set; get; }
        public int RoleId { set; get; }
        public virtual Role Role { set; get; }
        public virtual ICollection<AdminToken> AdminTokens { get; private set; }
    }
}
