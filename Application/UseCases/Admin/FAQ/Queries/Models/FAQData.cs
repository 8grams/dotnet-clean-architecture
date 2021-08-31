using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Models
{
    public class FAQData : BaseDtoData, IHaveCustomMapping
    {
        public string Question { set; get; }
        public string Answer { set; get; }
        public bool IsActive { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Faq, FAQData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Question, opt => opt.MapFrom(b => b.Question))
                .ForMember(bDto => bDto.Answer, opt => opt.MapFrom(b => b.Answer))
                .ForMember(bDto => bDto.IsActive, opt => opt.MapFrom(b => b.IsActive))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.CreateDate))
                .ForMember(bDto => bDto.UpdatedAt, opt => opt.MapFrom(b => b.LastUpdateDate));
        }
    }
}