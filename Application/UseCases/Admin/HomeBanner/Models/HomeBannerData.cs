using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Models
{
    public class HomeBannerData : BaseDtoData, IHaveCustomMapping
    {
        public string Name { set; get; }
        public ImageDescription Image { set; get; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.HomeBanner, HomeBannerData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Name, opt => opt.MapFrom(b => b.Name))
                .ForMember(bDto => bDto.Image, opt => opt.MapFrom(b => new ImageDescription
                {
                    ImageId = b.ImageId,
                    Link = ((AutoMapperProfile)configuration).GetFullUrl(b.Image.Link)
                }))
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