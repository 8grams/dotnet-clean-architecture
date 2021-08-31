using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.FAQ.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Queries.GetFAQ
{
    public class GetFAQDto : PaginationDto
    {
        public FAQData Data { set; get; }
    }
}
