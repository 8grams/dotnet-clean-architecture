using System.Linq;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Models
{
    public class InfoData : BaseDtoData, IHaveCustomMapping
    {
        public string Title { set; get; }
        public bool IsActive { set; get; }
        public bool IsDownloadable { set; get; }
        public bool IsRecommended { set; get; }
        public FileDescription File { set; get; }
        public int TotalViews { set; get; }
        public int TotalUserViewed { set; get; }
        public ImageDescription ImageThumbnail { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.AdditionalInfo, InfoData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Title, opt => opt.MapFrom(b => b.Title))
                .ForMember(bDto => bDto.IsActive, opt => opt.MapFrom(b => b.IsActive))
                .ForMember(bDto => bDto.IsDownloadable, opt => opt.MapFrom(b => b.IsDownloadable))
                .ForMember(bDto => bDto.IsRecommended, opt => opt.MapFrom(b => Resolve(((AutoMapperProfile)configuration)._context, b)))
                .ForMember(bDto => bDto.File, opt => opt.MapFrom(b => new FileDescription
                {
                    Code = b.FileCode,
                    Link = ((AutoMapperProfile)configuration).GetFullUrl(b.Link),
                    Type = b.FileType
                }))
                .ForMember(bDto => bDto.ImageThumbnail, opt => opt.MapFrom(b => new ImageDescription
                {
                    ImageId = b.ImageThumbnailId,
                    Link = ((AutoMapperProfile)configuration).GetFullUrl(b.ImageThumbnail.Link)
                }))
                .ForMember(bDto => bDto.TotalViews, opt => opt.MapFrom(b => b.TotalViews))
                .ForMember(bDto => bDto.TotalUserViewed, opt => opt.MapFrom(b => ResolveTotalUserViewed(((AutoMapperProfile)configuration)._context, b)))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.CreateDate))
                .ForMember(bDto => bDto.UpdatedAt, opt => opt.MapFrom(b => b.LastUpdateDate));
        }

        public static bool Resolve(ISFDDBContext context, Domain.Entities.AdditionalInfo source)
        {
            var recommendation = context.Recommendations
                .Where(e => e.ContentId == source.Id)
                .Where(e => e.ContentType.Equals(Domain.Entities.Recommendation.TYPE_INFO))
                .FirstOrDefault();
            return recommendation != null;
        }

        public static int ResolveTotalUserViewed(ISFDDBContext context, Domain.Entities.AdditionalInfo source)
        {
            var total = context.MaterialCounters
                .Where(e => e.ContentId == source.Id)
                .Where(e => e.ContentType.Equals(Domain.Entities.Recommendation.TYPE_INFO))
                .Select(x => new {x.ContentId, x.ContentType, x.UserId})
                .Distinct().Count();
            return total;
        }
    }

    public class ImageDescription
    {
        public int ImageId { set; get; }
        public string Link { set; get; }
    }

    public class FileDescription
    {
        public string Type { set; get; }
        public string Code { set; get; }
        public string Link { set; get; }
    }
}