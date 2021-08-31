using System;
using SFIDWebAPI.Domain.Entities;

namespace SFIDWebAPI.Domain.Infrastructures
{
    public interface IRecommendable
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string FileType { set; get; }
        public string Link { set; get; }
        public int ImageThumbnailId { set; get; }
        public int TotalViews { set; get; }
        public DateTime PublishedAt { set; get; }
        public DateTime CreateDate { set; get; }
        public DateTime ExpiresAt { set; get; }

        public ImageGallery ImageThumbnail { set; get; }
    }
}
