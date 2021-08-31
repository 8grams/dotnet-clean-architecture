using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.StaticContent.Command.UpdateContent
{
    public class UpdateContentCommand : BaseAdminQueryCommand, IRequest<UpdateContentDto>
    {
        public UpdateContentData Data { set; get; }
    }

    public class UpdateContentData
    {
        public string Name { set; get; }
        public string Content { set; get; }
    }
}
