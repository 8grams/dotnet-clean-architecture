using System;

namespace WebApi.Domain.Infrastructures
{
    public class BaseEntity
    {
        public int Id { set; get; }
        public string CreatedBy { set; get; }
        public DateTime CreatedDate { set; get; }
        public string LastUpdatedBy { set; get; }
        public DateTime? LastUpdatedDate { set; get; }
        public Int16? RowStatus { set; get; }
    }
}
