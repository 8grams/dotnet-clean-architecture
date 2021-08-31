using System;
using System.Collections.Generic;
using SFIDWebAPI.Domain.Infrastructures;

namespace SFIDWebAPI.Domain.Entities
{
	public class MasterCar : BaseEntity
	{
		public MasterCar()
		{
			GuideMaterials = new HashSet<GuideMaterial>();
			TrainingMaterials = new HashSet<TrainingMaterial>();
		}

		public string Title { set; get; }
		public string Tag { set; get; }
		public int ImageThumbnailId { set; get; }
		public int ImageCoverId { set; get; }
		public int ImageLogoId { set; get; }
		public bool TrainingActive { set; get; }
		public bool GuideActive { set; get; }
		public DateTime? TrainingLastUpdateDate { set; get; }
		public DateTime? GuideLastUpdateDate { set; get; }

		public virtual ImageGallery ImageThumbnail { set; get; }
        public virtual ImageGallery ImageCover { set; get; }
		public virtual ImageGallery ImageLogo { set; get; }

		public virtual ICollection<GuideMaterial> GuideMaterials { get; private set; }
		public virtual ICollection<TrainingMaterial> TrainingMaterials { get; private set; }
	}
}
