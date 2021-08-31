using System;

namespace SFIDWebAPI.Domain.Entities
{
    public class SalesmanMeta
    {
        public string SalesmanCode { set; get; }
        public string SalesmanName { set; get; }
        public string SalesmanEmail { set; get; }
        public string SalesmanHandphone { set; get; }
        public string SuperiorName { set; get; }
        public DateTime SalesmanHireDate { set; get; }
        public int JobPositionId { set; get; }
        public int SalesmanLevelId { set; get; }
        public int DealerBranchId { set; get; }
        public int PositionMetaId { set; get; }
        public Int16 GradeLastYear { set; get; }
        public Int16 GradeCurrentYear { set; get; }
        public virtual JobPosition JobPosition { set; get; }
        public virtual PositionMeta PositionMeta { set; get; }
        public virtual SalesmanLevel SalesmanLevel { set; get; }
        public virtual DealerBranch DealerBranch { set; get; }
        public virtual User User { set; get; }
    }
}