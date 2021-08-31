using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class JobPosition : BaseEntity
    {
        public JobPosition()
		{
			SalesmenMeta = new HashSet<SalesmanMeta>();
		}

        public string Code { set; get;  }
        public string Description { set; get; }
        public int Category { set; get; }
        public int SalesTarget { set; get; }

        public virtual ICollection<SalesmanMeta> SalesmenMeta { get; private set; }
    }
}
