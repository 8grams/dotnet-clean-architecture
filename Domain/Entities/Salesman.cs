using System;
using System.Collections.Generic;

namespace SFIDWebAPI.Domain.Entities
{
    public class Salesman
    {
        public Salesman()
        {
            PKTHistories = new HashSet<PKTHistory>();
            SalesmanGrades = new HashSet<SalesmanGrade>();
        }

        public Int16 Id { set; get; }
        public string DealerCode { set; get; }
        public string DealerName { set; get; }
        public string DealerCity { set; get; }
        public string DealerGroup { set; get; }
        public string DealerArea { set; get; }
        public string DealerBranchCode { set; get; }
        public string DealerBranchName { set; get; }
        public string SalesmanCode { set; get; }
        public string SalesmanName { set; get; }
        public DateTime SalesmanHireDate { set; get; }
        public string JobDescription { set; get; }
        public string LevelDescription { set; get; }
        public string SuperiorName { set; get; }
        public string SuperiorCode { set; get; }
        public string SalesmanEmail { set; get; }
        public string SalesmanHandphone { set; get; }
        public string SalesmanTeamCategory { set; get; }
        public string SalesmanStatus { set; get; }
        public DateTime LastUpdateTime { set; get; }
        public virtual User User { set; get; }
        public virtual ICollection<SalesmanGrade>  SalesmanGrades { set; get; }
        public virtual ICollection<PKTHistory> PKTHistories { get; private set; }
    }
}
