namespace WebApi.Application.Interfaces
{
    public interface IMemoryData
    {
        public int NewTraining { set; get; }
        public int NewGuide { set; get; }
        public int NewBulletin { set; get; }
        public int NewInfo { set; get; }
    }
}
