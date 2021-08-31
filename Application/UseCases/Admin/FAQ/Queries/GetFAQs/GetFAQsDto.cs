using System.Collections.Generic;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.FAQ.Models;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Queries.GetFAQs
{
    public class GetFAQsDto : PaginationDto
    {
        public IList<FAQData> Data { set; get; }
    }
}
