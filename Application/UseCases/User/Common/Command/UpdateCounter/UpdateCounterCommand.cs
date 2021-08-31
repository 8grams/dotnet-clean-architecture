using MediatR;

namespace SFIDWebAPI.Application.UseCases.User.Common.Command.UpdateCounter
{
    public class UpdateCounterCommand : IRequest<UpdateCounterDto>
    {
        public int UserId { set; get; }
        public UpdateCounterCommandData Data { set; get; }
    }

    public class UpdateCounterCommandData
    {
        public int FileId { set; get; }
        public string FileType { set; get; }
    }
}
