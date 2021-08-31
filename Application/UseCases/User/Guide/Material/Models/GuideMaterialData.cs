using AutoMapper;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.User.Guide.Material.Models
{
    public class GuideMaterialData : BaseDtoData, IHaveCustomMapping
    {
        public string Title { set; get; }
        public string Type { set; get; }
        public string Link { set; get; }
        public string Status { set; get; }
        public int TotalViews { set; get; }
        public ImageDto Images { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.GuideMaterial, GuideMaterialData>()
                .ForMember(cDto => cDto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(cDto => cDto.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(cDto => cDto.Type, opt => opt.MapFrom(c => c.FileType))
                .ForMember(cDto => cDto.Link, opt => opt.MapFrom(c => ((AutoMapperProfile)configuration).GetFullUrl(c.Link) ))
                .ForMember(cDto => cDto.TotalViews, opt => opt.MapFrom(c => c.TotalViews))
                .ForMember(cDto => cDto.Images, opt => opt.MapFrom(c => new ImageDto
                {
                    Cover = ((AutoMapperProfile)configuration).GetFullUrl(c.ImageThumbnail.Link),
                    Thumbnail = ((AutoMapperProfile)configuration).GetFullUrl(c.ImageThumbnail.Link)
                }))
                .ForMember(cDto => cDto.CreatedAt, opt => opt.MapFrom(c => c.CreateDate))
                .ForMember(cDto => cDto.UpdatedAt, opt => opt.MapFrom(c => c.LastUpdateDate));
        }
    }
}
