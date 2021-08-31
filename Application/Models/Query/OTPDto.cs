using AutoMapper;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Domain.Entities;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.Models.Query
{
    public class OTPDto : BaseDto
    {
        public OTPData Data { set; get; }
    }

    public class OTPData : IHaveCustomMapping
    {
        public string AuthToken { set; get; }
        public string Type { set; get; }
        public long ExpiresAt { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AccessToken, OTPData>()
                .ForMember(oDto => oDto.AuthToken, opt => opt.MapFrom(a => a.AuthToken))
                .ForMember(oDto => oDto.Type, opt => opt.MapFrom(a => "Bearer"))
                .ForMember(oDto => oDto.ExpiresAt, opt => opt.MapFrom(a => Utils.DateToTimestamps(a.ExpiresAt)));
        }
    }
}
