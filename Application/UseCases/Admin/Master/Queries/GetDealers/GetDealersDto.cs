using System.Collections.Generic;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealers
{
    public class GetDealersDto : BaseDto
    {
        public IList<DealerData> Data { set; get; }
    }

    public class DealerData : IHaveCustomMapping
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Dealer, DealerData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Name, opt => opt.MapFrom(b => b.DealerName));
        }
    }
}
