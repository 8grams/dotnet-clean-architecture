using System;

namespace SFIDWebAPI.Application.UseCases.User.PKT.Models
{
    public class VehicleInfo 
    {
        public string Vin { set; get; }
        public string VehicleType { set; get; }
        public string VehicleColor { set; get; }
    }

    public class SalesInfo
    {
        public string SalesCode { set; get; }
        public string Name { set; get; }
        public string DealerName { set; get; }
    }

    public class VinDataSP
    {
        public string SoldDealerCode { set; get; }
        public string SoldDealerName { set; get; }
        public string ChassisNumber { set; get; }
        public string SalesmanCode { set; get; }
        public string SalesName { set; get; }
        public string SPKNumber { set; get; }
        public string SPKDealerCode { set; get; }
        public DateTime? TglPKT { set; get; }
        public DateTime? PDIDate { set; get; }
        public string CustomerName { set;get; }
        public string FakturStatus { set; get; }
        public string VehicleColor { set; get; }
        public string Description { set; get; }
    }
}