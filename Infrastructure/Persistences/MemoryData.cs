using WebApi.Application.Interfaces;

namespace WebApi.Infrastructure.Persistences
{
    public class MemoryData : IMemoryData
    {
        public int NewTraining { set; get; }
        public int NewGuide { set; get; }
        public int NewBulletin { set; get; }
        public int NewInfo { set; get; }
    }
}
