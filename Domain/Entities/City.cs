using System;
using System.Collections.Generic;

namespace SFIDWebAPI.Domain.Entities
{
    public class City
    {
        public City()
		{
			Dealers = new HashSet<Dealer>();
            DealerBranches = new HashSet<DealerBranch>();
		}

        public Int16 Id { set; get; }
        public int ProvinceId { set; get; }
        public string CityCode { set; get; }
        public string CityName { set; get; }
        public DateTime LastUpdateTime { set; get; }
        public string Status { set; get; }

        public virtual ICollection<Dealer> Dealers { get; private set; }
        public virtual ICollection<DealerBranch> DealerBranches { get; private set; }
    }
}