using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.User.Common.Command.UpdateCounter
{
    public class UpdateCounterDto : BaseDto
    {
        public ProfileDto Data { set; get; }
    }
}
