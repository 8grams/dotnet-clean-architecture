using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.UpdateFAQ
{
    public class UpdateFAQCommand : BaseAdminQueryCommand, IRequest<UpdateFAQDto>
    {
        public UpdateFAQData Data { set; get; }
    }

    public class UpdateFAQData
    {
        public int Id { set; get; }
        public string Question { set; get; }
        public string Answer { set; get; }
        public bool IsActive { set; get; }
    }
}
