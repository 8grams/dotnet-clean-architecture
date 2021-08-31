using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
    public class ImageGallery : BaseEntity
    {
        public ImageGallery()
        {
            AdditionalInfoImageThumbnails = new HashSet<AdditionalInfo>();
            BulletinInfoImageThumbnails = new HashSet<Bulletin>();
            GuideMaterialImageThumbnails = new HashSet<GuideMaterial>();
            TrainingMaterialImageThumbnails = new HashSet<TrainingMaterial>();
            HomeBannerImages = new HashSet<HomeBanner>();
            MasterCarImageCovers = new HashSet<MasterCar>();
            MasterCarImageThumbnails = new HashSet<MasterCar>();
            MasterCarImageLogos = new HashSet<MasterCar>();
        }

        public string Name { set; get; }
        public string Link { set; get; }
        public string Category { set; get; }

        public virtual ICollection<AdditionalInfo> AdditionalInfoImageThumbnails { get; private set; }
        public virtual ICollection<Bulletin> BulletinInfoImageThumbnails { get; private set; } 
        public virtual ICollection<MasterCar> MasterCarImageCovers { get; private set; } 
        public virtual ICollection<MasterCar> MasterCarImageThumbnails { get; private set; }
        public virtual ICollection<MasterCar> MasterCarImageLogos { get; private set; } 
        public virtual ICollection<GuideMaterial> GuideMaterialImageThumbnails { get; private set; } 
        public virtual ICollection<TrainingMaterial> TrainingMaterialImageThumbnails { get; private set; } 
        public virtual ICollection<HomeBanner> HomeBannerImages { get; private set; } 
    }
}
