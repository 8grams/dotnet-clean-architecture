using System;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class HomeBanner : BaseEntity
    {
        public string Name { set; get; }
        public int ImageId { set; get; }
        public DateTime PublishedAt { set; get; }
        public DateTime ExpiresAt { set; get; }

        public virtual ImageGallery Image { set; get; }
    }
}
