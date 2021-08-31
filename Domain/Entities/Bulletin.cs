using System;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
	public class Bulletin : BaseEntity, IRecommendable
    {
        public string Title { set; get; }
        public string Link { set; get; }
        public string FileCode { set; get; }
        public string FileType { set; get; }
        public int ImageThumbnailId { set; get; }
        public int TotalViews { set; get; }
        public bool IsActive { set; get; }
        public DateTime PublishedAt { set; get; }
        public DateTime ExpiresAt { set; get; }
        public virtual ImageGallery ImageThumbnail { set; get; }
    }
}
