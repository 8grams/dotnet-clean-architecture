using System.Linq;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Models
{
    public class TrainingMaterialData : BaseDtoData, IHaveCustomMapping
    {
        public string Title { set; get; }
        public int CategoryId { set; get; }
        public File File { set; get; }
        public bool IsRecommended { set; get; }
        public int TotalViews { set; get; }
        public int TotalUserViewed { set; get; }
        public ImageDescription ImageThumbnail { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.TrainingMaterial, TrainingMaterialData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.CategoryId, opt => opt.MapFrom(b => b.MasterCarId))
                .ForMember(bDto => bDto.Title, opt => opt.MapFrom(b => b.Title))
                .ForMember(bDto => bDto.File, opt => opt.MapFrom(b => Resolve( (AutoMapperProfile)configuration, b) ))
                .ForMember(bDto => bDto.ImageThumbnail, opt => opt.MapFrom(b => new ImageDescription
                {
                    ImageId = b.ImageThumbnailId,
                    Link = ((AutoMapperProfile)configuration).GetFullUrl(b.ImageThumbnail.Link)
                }))
                .ForMember(bDto => bDto.IsRecommended, opt => opt.MapFrom(b => Resolve(((AutoMapperProfile)configuration)._context, b)))
                .ForMember(bDto => bDto.TotalViews, opt => opt.MapFrom(b => b.TotalViews))
                .ForMember(bDto => bDto.TotalUserViewed, opt => opt.MapFrom(b => ResolveTotalUserViewed(((AutoMapperProfile)configuration)._context, b)))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.CreateDate))
                .ForMember(bDto => bDto.UpdatedAt, opt => opt.MapFrom(b => b.LastUpdateDate));
        }

        public static bool Resolve(ISFDDBContext context, Domain.Entities.TrainingMaterial source)
        {
            var recommendation = context.Recommendations
                .Where(e => e.ContentId == source.Id)
                .Where(e => e.ContentType.Equals(Domain.Entities.Recommendation.TYPE_TRAINING))
                .FirstOrDefault();

            return recommendation != null;
        }

        public static File Resolve(AutoMapperProfile profile, Domain.Entities.TrainingMaterial source)
        {
            var link = profile.GetFullUrl(source.Link); // get link
            var fileName = link.Split("_").Last(); // get type
            var type = source.FileType; // get type

            return new File()
            {
                Name = fileName,
                Type = source.FileType,
                Link = link,
                Code = source.FileCode
            };
        }

        public static int ResolveTotalUserViewed(ISFDDBContext context, Domain.Entities.TrainingMaterial source)
        {
            var total = context.MaterialCounters
                .Where(e => e.ContentId == source.Id)
                .Where(e => e.ContentType.Equals(Domain.Entities.Recommendation.TYPE_TRAINING))
                .Select(x => new {x.ContentId, x.ContentType, x.UserId})
                .Distinct().Count();
            return total;
        }
    }

    public class File
    {
        public string Name { set; get; }
        public string Type { set; get; }
        public string Link { set; get; }
        public string Code { set; get; }
    }

    public class ImageDescription
    {
        public int ImageId { set; get; }
        public string Link { set; get; }
    }
}