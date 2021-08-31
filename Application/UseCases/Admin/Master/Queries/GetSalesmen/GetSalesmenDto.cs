
using System;
using System.Collections.Generic;
using AutoMapper;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Mappings;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetSalesmen
{
    public class GetSalesmenDto : BaseDto
    {
        public IList<SalesmanData> Data { set; get; }
    }

    public class SalesmanData : IHaveCustomMapping
    {
        public Int16 Id { set; get; }
        public string SalesCode { set; get; }
        public string Name { set; get; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Entities.Salesman, SalesmanData>()
                .ForMember(bDto => bDto.Id, opt => opt.MapFrom(b => b.Id))
                .ForMember(bDto => bDto.SalesCode, opt => opt.MapFrom(b => b.SalesmanCode))
                .ForMember(bDto => bDto.Name, opt => opt.MapFrom(b => b.SalesmanName));
        }
    }
}
