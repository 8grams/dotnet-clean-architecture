using System;
namespace WebApi.Application.Models.Query
{
    public class BaseDtoData
    {
        public int Id { set; get; }
        public DateTime? CreatedAt { set; get; }
        public DateTime? UpdatedAt { set; get; }
    }
}
