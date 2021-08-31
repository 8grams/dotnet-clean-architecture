using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Command.ImportUser
{
    public class ImportUserDto : BaseDto
    {
        
    }

    public class ImportData
    {
        public string SalesmanCode { set; get; }
        public string SalesmanEmail { set; get; }
        public string SalesmanName { set; get; }
        public string JobDescription { set; get; }
        public string DealerBranchCode { set; get; }
        public string DealerCode { set; get; }
    }
}