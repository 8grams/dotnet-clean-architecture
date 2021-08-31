using System.Collections.Generic;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealerGroups
{
    public class GetDealerGroupsDto : BaseDto
    {
        public IList<DealerGroupData> Data { set; get; }
    }

    public class DealerGroupData : IHaveCustomMapping
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.DealerGroup, DealerGroupData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Name, opt => opt.MapFrom(b => b.GroupName));
        }
    }
}
