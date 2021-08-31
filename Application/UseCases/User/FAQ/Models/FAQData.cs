using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.User.FAQ.Models
{
    public class FAQData : BaseDtoData, IHaveCustomMapping
    {
        public string Answer { set; get; }
        public string Question { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Faq, FAQData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Answer, opt => opt.MapFrom(b => b.Answer))
                .ForMember(bDto => bDto.Question, opt => opt.MapFrom(b => b.Question))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.CreateDate))
                .ForMember(bDto => bDto.UpdatedAt, opt => opt.MapFrom(b => b.LastUpdateDate));
        }
    }
}
