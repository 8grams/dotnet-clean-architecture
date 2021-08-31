using System;
using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class Dealer : BaseEntity
    {
        public Dealer()
		{
			DealerBranches = new HashSet<DealerBranch>();
            Users = new HashSet<User>();
            UserPresences = new HashSet<UserPresence>();
		}

        public Int16 MainDealerId { set; get;  }
        public string DealerCode { set; get; }
        public string DealerName { set; get; }
        public string Status { set; get; }
        public string Title { set; get;}
        public string SearchTerm1 { set; get; }
        public string SearchTerm2 { set; get; }
        public int? DealerGroupId { set; get; }
        public string Address { set; get; }
        public Int16 CityId { set; get; }
        public string ZipCode { set; get; }
        public int ProvinceId { set; get; }
        public string Phone { set; get; }
        public string Fax { set; get; }
        public string Website { set; get; }
        public string Email { set; get; }
        public string SalesUnitFlag { set; get; }
        public string ServiceFlag { set; get; }
        public string SparepartFlag { set; get; }
        public int? Area1Id { set; get; }
        public int? Area2Id { set; get; }
        public string SPANumber { set; get; }
        public DateTime SPADate { set; get; }
        public int FreePPh22Indicator { set; get; }
        public DateTime FreePPh22From { set; get; }
        public DateTime FreePPh22To { set; get; }
        public string LegalStatus { set; get; }
        public int DueDate { set; get; }
        public string AgreementNo { set; get; }
        public DateTime AgreementDate { set; get; }
        public string CreditAccount { set; get; }
        public int? MainAreaId { set; get; }

        public virtual City City { set; get; }
        public virtual DealerGroup DealerGroup { set; get; }
        public virtual ICollection<DealerBranch> DealerBranches { get; private set; }
        public virtual ICollection<User> Users { get; private set; }
        public virtual ICollection<UserPresence> UserPresences { get; private set; }
    }
}
