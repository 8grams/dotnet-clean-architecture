using System;
using System.Linq;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces.Mappings;
using SFIDWebAPI.Application.UseCases.User.PKT.Models;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Infrastructures.AutoMapper;

namespace SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.VerifyVin
{
    public class VerifyVinDto : BaseDto
    {
        public VerifyVinData Data { set; get; }
    }

    public class VerifyVinData : IHaveCustomMapping
    {
        public VehicleInfo VehicleInfo { set; get; }
        public SalesInfo SalesInfo { set; get; }
        public DateTime? PDIDate { set; get; }
        public DateTime? CreatedAt { set; get; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<VinDataSP, VerifyVinData>()
                .ForMember(nDto => nDto.PDIDate, opt => opt.MapFrom(n => n.PDIDate))
                .ForMember(nDto => nDto.CreatedAt, opt => opt.MapFrom(n => Resolve(((AutoMapperProfile)configuration)._context, n.ChassisNumber)))
                .ForMember(nDto => nDto.VehicleInfo, opt => opt.MapFrom(n => new VehicleInfo
                {
                    Vin = n.ChassisNumber,
                    VehicleColor = n.VehicleColor,
                    VehicleType = n.Description
                }))
                .ForMember(nDto => nDto.SalesInfo, opt => opt.MapFrom(n => new SalesInfo
                {
                    DealerName = n.SoldDealerName,
                    SalesCode = n.SalesmanCode,
                    Name = n.SalesName
                }));
        }

        public static DateTime? Resolve(ISFDDBContext context, String vin)
        {
            var exists = context.PKTHistories
                .Where(e => e.Vin.Equals(vin))
                .FirstOrDefault();

            if (exists != null)
            {
                return exists.CreateDate;
            }

            return null;
        }
    }
}
