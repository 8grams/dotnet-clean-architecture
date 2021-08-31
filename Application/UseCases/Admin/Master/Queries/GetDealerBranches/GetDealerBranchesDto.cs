using System;
using System.Collections.Generic;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealerBranches
{
    public class GetDealerBranchesDto : BaseDto
    {
        public IList<DealerBranchData> Data { set; get; }
    }

    public class DealerBranchData : IHaveCustomMapping
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int DealerId { set; get; }
        public string DealerName { set; get; }
        public Int16 CityId { set; get; }
        public string CityName { set; get; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.DealerBranch, DealerBranchData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.Name, opt => opt.MapFrom(b => b.Name))
                .ForMember(bDto => bDto.DealerId, opt => opt.MapFrom(b => b.Dealer.Id))
                .ForMember(bDto => bDto.DealerName, opt => opt.MapFrom(b => b.Dealer.DealerName))
                .ForMember(bDto => bDto.CityId, opt => opt.MapFrom(b => b.City.Id))
                .ForMember(bDto => bDto.CityName, opt => opt.MapFrom(b => b.City.CityName));
        }
    }
}
