using System;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class SalesmanGrade : BaseEntity
    {

        public Int16 SalesmanHeaderID { set; get; }
        public Int16 Year { set; get; }
        public Int16 Period { set; get; }
        public Int16 Grade { set; get; }
        public Int16 Status { set; get; }
        public virtual Salesman Salesman { set; get; }

    }
}