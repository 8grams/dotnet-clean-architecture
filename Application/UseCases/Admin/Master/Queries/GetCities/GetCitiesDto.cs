using System;
using System.Collections.Generic;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetCities
{
    public class GetCitiesDto : BaseDto
    {
        public IList<CityData> Data { set; get; }
    }

    public class CityData : IHaveCustomMapping
    {
        public Int16 Id { set; get; }
        public string Name { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.City, CityData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Name, opt => opt.MapFrom(b => b.CityName));
        }
    }
}
