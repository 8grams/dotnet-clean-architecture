using System;
using System.Collections.Generic;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Queries.GetAdmins
{
    public class GetAdminsDto : PaginationDto
    {
        public IList<AdminData> Data { set; get; }
    }

    public class AdminData : BaseDtoData, IHaveCustomMapping
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Role { set; get; }
        public DateTime? LastLogin {set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Admin, AdminData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Name, opt => opt.MapFrom(b => b.Name))
                .ForMember(bDto => bDto.Email, opt => opt.MapFrom(b => b.Email))
                .ForMember(bDto => bDto.Phone, opt => opt.MapFrom(b => b.Phone))
                .ForMember(bDto => bDto.CreatedAt, opt => opt.MapFrom(b => b.CreateDate))
                .ForMember(bDto => bDto.LastLogin, opt => opt.MapFrom(b => b.LastLogin))
                .ForMember(bDto => bDto.Role, opt => opt.MapFrom(b => b.Role.Name));
        }
    }
}
