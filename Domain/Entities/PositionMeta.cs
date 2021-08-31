using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class PositionMeta : BaseEntity
    {
        public PositionMeta()
        {
            SalesmenMeta = new HashSet<SalesmanMeta>();
        }

        public string Code { set; get; }
        public string Description { set; get; }

        public virtual ICollection<SalesmanMeta> SalesmenMeta { get; private set; }
    }
}