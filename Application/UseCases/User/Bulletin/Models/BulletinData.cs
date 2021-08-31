using System.Collections.Generic;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.User.Bulletin.Models
{
    public class BulletinData : BaseDtoData, IHaveCustomMapping
    {
        public string Title { set; get; }
        public string Type { set; get; }
        public string Link { set; get; }
        public string PublishedAt { set; get; }
        public int TotalViews { set; get; }
        public ImageDto Images { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Bulletin, BulletinData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Title, opt => opt.MapFrom(b => b.Title))
                .ForMember(bDto => bDto.Type, opt => opt.MapFrom(b => b.FileType))
                .ForMember(bDto => bDto.Link, opt => opt.MapFrom(b => ((AutoMapperProfile)configuration).GetFullUrl(b.Link) ))
                .ForMember(bDto => bDto.PublishedAt, opt => opt.MapFrom(b => b.PublishedAt))
                .ForMember(bDto => bDto.TotalViews, opt => opt.MapFrom(b => b.TotalViews))
                .ForMember(bDto => bDto.Images, opt => opt.MapFrom(u => new ImageDto
                {
                    Cover = ((AutoMapperProfile)configuration).GetFullUrl(u.ImageThumbnail.Link),
                    Thumbnail = ((AutoMapperProfile)configuration).GetFullUrl(u.ImageThumbnail.Link)
                }))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.CreateDate))
                .ForMember(bDto => bDto.UpdatedAt, opt => opt.MapFrom(b => b.LastUpdateDate));
        }
    }
}
