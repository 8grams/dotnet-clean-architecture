using System;
using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class DealerBranch : BaseEntity
    {
        public DealerBranch()
		{
			SalesmenMeta = new HashSet<SalesmanMeta>();
		}
        public int DealerId { set; get;  }
        public string Name { set; get; }
        public string Status { set; get; }
        public string Address { set; get; }
        public Int16 CityId { set; get;}
        public string ZipCode { set; get; }
        public int ProvinceId { set; get; }
        public string Phone { set; get; }
        public string Fax { set; get; }
        public string Website { set; get; }
        public string Email { set; get; }
        public string TypeBranch { set; get; }
        public string DealerBranchCode { set; get; }
        public string Term1 { set; get; }
        public string Term2 { set; get; }
        public int MainAreaId { set; get; }
        public int Area1Id { set; get; }
        public int Area2Id { set; get; }
        public string BranchAssignmentNo { set; get; }
        public DateTime BranchAssignmentDate { set; get; }
        public string SalesUnitFlag { set; get; }
        public string ServiceFlag { set; get; }
        public string SparePartFlag { set; get; }
        public virtual Dealer Dealer { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<SalesmanMeta> SalesmenMeta { get; private set; }
    }
}
