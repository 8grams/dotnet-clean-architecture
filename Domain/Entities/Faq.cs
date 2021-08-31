using System;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class Faq : BaseEntity
    {
        public string Question { set; get; }
        public string Answer { set; get; }
        public bool IsActive { set; get; }
    }
}
