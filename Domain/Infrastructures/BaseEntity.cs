using System;

namespace WebApi.Domain.Infrastructures
{
    public class BaseEntity
    {
        public int Id { set; get; }
        public string CreateBy { set; get; }
        public DateTime CreateDate { set; get; }
        public string LastUpdateBy { set; get; }
        public DateTime? LastUpdateDate { set; get; }
        public Int16? RowStatus { set; get; }
    }
}
