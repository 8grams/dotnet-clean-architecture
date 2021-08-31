using System.Collections.Generic;
using SFIDWebAPI.Application.UseCases.User.FAQ.Models;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.FAQ.Queries.GetFAQList
{
    public class GetFAQListDto : BaseDto
    {
        public IList<FAQData> Data { set; get; }
    }
}