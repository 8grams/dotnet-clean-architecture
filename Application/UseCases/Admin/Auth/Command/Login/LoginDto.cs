using AutoMapper;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Domain.Entities;
using SFIDWebAPI.Application.Misc;
using SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.Auth.Command.Login
{
    public class LoginDto : BaseDto
    {
        public TokenData Data { set; get; }
    }

    public class TokenData : IHaveCustomMapping
    {
        public string Token { set; get; }
        public string Type { set; get; }
        public long ExpiresAt { set; get; }
        public AdminProfileData Profile { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<AdminToken, TokenData>()
                .ForMember(aDto => aDto.Token, opt => opt.MapFrom(u => u.AuthToken))
                .ForMember(aDto => aDto.Type, opt => opt.MapFrom(u => "Bearer"))
                .ForMember(aDto => aDto.Profile, opt => opt.MapFrom(u => new AdminProfileData() {
                    Id = u.Admin.Id,
                    Name = u.Admin.Name,
                    Email = u.Admin.Email
                }))
                .ForMember(aDto => aDto.ExpiresAt, opt => opt.MapFrom(u => Utils.DateToTimestamps(u.ExpiresAt)));
        }
    }
}