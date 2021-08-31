using System;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class GuideMaterial : BaseEntity, IRecommendable
    {
        public string Title { set; get; }
        public int ImageThumbnailId { set; get; }
        public string Link { set; get; }
        public string FileType { set; get; }
        public string FileCode { set; get; }
        public int TotalViews { set; get; }
        public int MasterCarId { set; get; }
        public DateTime PublishedAt { set; get; }
        public DateTime ExpiresAt { set; get; }

        public virtual MasterCar MasterCar { set; get; }
        public virtual ImageGallery ImageThumbnail { set; get; }

    }
}
