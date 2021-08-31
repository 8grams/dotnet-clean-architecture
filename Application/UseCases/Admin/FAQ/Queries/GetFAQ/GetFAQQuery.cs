using System;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Queries.GetFAQ
{
    public class GetFAQQuery : BaseAdminQueryCommand, IRequest<GetFAQDto>
    {
        public int Id { set; get; }
    }
}
