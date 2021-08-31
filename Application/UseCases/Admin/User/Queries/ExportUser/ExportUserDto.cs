using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Queries.ExportUser
{
    public class ExportUserDto : BaseDto
    {
        public ExportUserData Data { set; get; }
    }

    public class ExportUserData
    {
        public string Link { set; get; }
    }

    public class ExportData
    {
    #nullable enable
        public string? SalesmanCode { set; get; }
        public string? SalesmanName { set; get; }
        public string? SalesmanEmail { set; get; }
        public string? SalesmanHandphone { set; get; }
        public string? JobDescription { set; get; }
        public string? DealerCode { set; get; }
        public string? DealerName { set; get; }
        public string? DealerCity { set; get; }
        public string? DealerBranchCode { set; get; }
        public string? Grade { set; get; }
        public string? SalesmanHireDate { set; get; }
        public string? SalesmanStatus { set; get; }
    }
}