using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Command.UpdatePositionMeta
{
    public class UpdatePositionMetaCommand : BaseAdminQueryCommand, IRequest<UpdatePositionMetaDto>
    {
        public UpdatePositionMetaData Data { set; get; }
    }

    public class UpdatePositionMetaData
    {
        public int Id { set; get; }
        public string Code { set; get; }
        public string Description { set; get; }
    }
}
