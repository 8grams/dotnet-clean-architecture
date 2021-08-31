using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Models
{
    public class ImageGalleryData : BaseDtoData, IHaveCustomMapping
    {
        public string Name { set; get; }
        public string Category { set; get; }
        public string Link { set; get; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.ImageGallery, ImageGalleryData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Name, opt => opt.MapFrom(b => b.Name))
                .ForMember(bDto => bDto.Category, opt => opt.MapFrom(b => b.Category))
                .ForMember(bDto => bDto.Link, opt => opt.MapFrom(b => ((AutoMapperProfile)configuration).GetFullUrl(b.Link) ))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.CreateDate))
                .ForMember(bDto => bDto.UpdatedAt, opt => opt.MapFrom(b => b.LastUpdateDate));
        }
    }


}