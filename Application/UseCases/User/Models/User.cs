using AutoMapper;
using WebApi.Application.Models.Query;
using WebApi.Application.Interfaces.Mappings;

namespace WebApi.Application.UseCases.User.Models
{
    public class UserData : BaseDtoData, IHaveCustomMapping
    {
        public string Name { set; get; }
        public string UserName { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int Age { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.User, UserData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Name, opt => opt.MapFrom(b => b.Name))
                .ForMember(bDto => bDto.UserName, opt => opt.MapFrom(b => b.UserName))
                .ForMember(bDto => bDto.Phone, opt => opt.MapFrom(b => b.Phone))
                .ForMember(bDto => bDto.Email, opt => opt.MapFrom(b => b.Email))
                .ForMember(bDto => bDto.Age, opt => opt.MapFrom(b => b.Age))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.CreatedDate))
                .ForMember(bDto => bDto.UpdatedAt, opt => opt.MapFrom(b => b.LastUpdatedDate));
        }
    }
}