using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Infrastructure.Persistences
{
    public class MemoryData : IMemoryData
    {
        public int NewTraining { set; get; }
        public int NewGuide { set; get; }
        public int NewBulletin { set; get; }
        public int NewInfo { set; get; }
    }
}
