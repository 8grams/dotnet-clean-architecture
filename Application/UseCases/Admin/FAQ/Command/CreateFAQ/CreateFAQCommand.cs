using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.CreateFAQ
{
    public class CreateFAQCommand : BaseAdminQueryCommand, IRequest<CreateFAQDto>
    {
        public CreateFAQData Data { set; get; }
    }

    public class CreateFAQData
    {
        public string Question { set; get; }
        public string Answer { set; get; }
        public bool IsActive { set; get; }
    }
}
