using System.Linq;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.User.Training.Category.Models
{
    public class TrainingCategoryData : BaseDtoData, IHaveCustomMapping
    {
        public string Title { set; get; }
        public int TotalMaterials { set; get; }
        public SubTotalMaterials SubTotalMaterials { set; get; }
        public string Tag { set; get; }
        public ImageDto Images { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.MasterCar, TrainingCategoryData>()
                .ForMember(cDto => cDto.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(cDto => cDto.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(cDto => cDto.Tag, opt => opt.MapFrom(c => c.Tag))
                .ForMember(cDto => cDto.TotalMaterials, opt => opt.MapFrom(c => c.TrainingMaterials.Count))
                .ForMember(cDto => cDto.Images, opt => opt.MapFrom(c => new ImageDto
                {
                    Cover = ((AutoMapperProfile)configuration).GetFullUrl(c.ImageCover.Link),
                    Thumbnail = ((AutoMapperProfile)configuration).GetFullUrl(c.ImageThumbnail.Link),
                    Logo = ((AutoMapperProfile)configuration).GetFullUrl(c.ImageLogo.Link)
                }))
                .ForMember(cDto => cDto.SubTotalMaterials, opt => opt.MapFrom(c => new SubTotalMaterials
                {
                    Reading = c.TrainingMaterials.Where(e => e.FileType != "mp4").Count(),
                    Video = c.TrainingMaterials.Where(e => e.FileType == "mp4").Count(),
                }))
                .ForMember(cDto => cDto.CreatedAt, opt => opt.MapFrom(c => c.CreateDate))
                .ForMember(cDto => cDto.UpdatedAt, opt => opt.MapFrom(c => c.LastUpdateDate));

        }
    }

    public class SubTotalMaterials
    {
        public int Reading { set; get; }
        public int Video { set; get; }
    }
}
