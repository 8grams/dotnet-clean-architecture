using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class DealerGroup : BaseEntity
    {
        public DealerGroup()
		{
			Dealers = new HashSet<Dealer>();
		}
        public string DealerGroupCode { set; get; }
        public string GroupName { set; get; }

        public virtual ICollection<Dealer> Dealers { get; private set; }

    }
}
