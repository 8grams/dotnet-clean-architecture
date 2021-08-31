using System;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class PKTHistory : BaseEntity
    {
        public string Vin { set; get; }
        public string Image { set; get; }
        public string SalesCode { set; get; }
        public DateTime PDIDate { set; get; }
        public string Note { set; get; }
        public string ReportType { set; get; } // can be scan or manual

        // 0 = draft
        // 1 = success
        // 2 = failed
        public int Status { set; get; }

        public virtual Salesman Salesman { set; get; }
    }
}