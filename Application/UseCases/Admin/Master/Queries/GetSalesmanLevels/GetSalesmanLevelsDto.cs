using System.Collections.Generic;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetSalesmanLevels
{
    public class GetSalesmanLevelsDto : BaseDto
    {
        public IList<SalesmanLevelData> Data { set; get; }
    }

    public class SalesmanLevelData : IHaveCustomMapping
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.SalesmanLevel, SalesmanLevelData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Name, opt => opt.MapFrom(b => b.Description));
        }
    }
}
