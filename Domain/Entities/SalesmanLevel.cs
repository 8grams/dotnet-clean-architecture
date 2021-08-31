using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class SalesmanLevel : BaseEntity
    {
        public SalesmanLevel()
		{
			SalesmenMeta = new HashSet<SalesmanMeta>();
		}
        public string Description { set; get; }
        public virtual ICollection<SalesmanMeta> SalesmenMeta { get; private set; }
    }
}