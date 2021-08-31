using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Command.CreatePositionMeta
{
    public class CreatePositionMetaCommand : BaseAdminQueryCommand, IRequest<CreatePositionMetaDto>
    {
        public CreatePositionMetaData Data { set; get; }
    }

    public class CreatePositionMetaData
    {
        public string Code { set; get; }
        public string Description { set; get; }
    }
}
