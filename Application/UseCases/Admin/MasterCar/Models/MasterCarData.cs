using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Models
{
    public class MasterCarData : BaseDtoData, IHaveCustomMapping
    {
        public string Name { set; get; }
        public string Tag { set; get; }
        public int TotalTrainingFiles { set; get; }
        public int TotalGuideFiles { set; get; }
        public bool TrainingActive { set; get; }
        public bool GuideActive { set; get; }
        public ImageDescription ImageThumbnail { set; get; }
        public ImageDescription ImageCover { set; get; }
        public ImageDescription ImageLogo { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.MasterCar, MasterCarData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Name, opt => opt.MapFrom(b => b.Title))
                .ForMember(bDto => bDto.Tag, opt => opt.MapFrom(b => b.Tag))
                .ForMember(bDto => bDto.TrainingActive, opt => opt.MapFrom(b => b.TrainingActive))
                .ForMember(bDto => bDto.GuideActive, opt => opt.MapFrom(b => b.GuideActive))
                .ForMember(bDto => bDto.ImageThumbnail, opt => opt.MapFrom(b => new ImageDescription
                {
                    ImageId = b.ImageThumbnailId,
                    Link = ((AutoMapperProfile)configuration).GetFullUrl(b.ImageThumbnail.Link)
                }))
                .ForMember(bDto => bDto.ImageCover, opt => opt.MapFrom(b => new ImageDescription
                {
                    ImageId = b.ImageCoverId,
                    Link = ((AutoMapperProfile)configuration).GetFullUrl(b.ImageCover.Link)
                }))
                .ForMember(bDto => bDto.ImageLogo, opt => opt.MapFrom(b => new ImageDescription
                {
                    ImageId = b.ImageLogoId,
                    Link = ((AutoMapperProfile)configuration).GetFullUrl(b.ImageLogo.Link)
                }))
                .ForMember(bDto => bDto.TotalTrainingFiles, opt => opt.MapFrom(b => b.TrainingMaterials.Count))
                .ForMember(bDto => bDto.TotalGuideFiles, opt => opt.MapFrom(b => b.GuideMaterials.Count))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.CreateDate))
                .ForMember(bDto => bDto.UpdatedAt, opt => opt.MapFrom(b => b.LastUpdateDate));
        }
    }

    public class ImageDescription
    {
        public int ImageId { set; get; }
        public string Link { set; get; }
    }
}