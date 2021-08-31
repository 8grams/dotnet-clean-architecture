using AutoMapper;
using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.User.HomeBanner.Queries.GetHomeBannerList
{
    public class HomeBannerListDto : PaginationDto
    {
        public IList<HomeBannerData> Data { set; get; }
    }

    public class HomeBannerData : BaseDtoData, IHaveCustomMapping
    {
        public string Image { set; get; }
        public string PublishedAt { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.HomeBanner, HomeBannerData>()
                .ForMember(nDto => nDto.Id, opt => opt.MapFrom(n => n.Id))
                .ForMember(nDto => nDto.Image, opt => opt.MapFrom(n => ((AutoMapperProfile)configuration).GetFullUrl(n.Image.Link) ))
                .ForMember(nDto => nDto.PublishedAt, opt => opt.MapFrom(n => n.PublishedAt))
                .ForMember(nDto => nDto.CreatedAt, opt => opt.MapFrom(n => n.CreateDate))
                .ForMember(nDto => nDto.UpdatedAt, opt => opt.MapFrom(n => n.LastUpdateDate));
        }
    }
}
