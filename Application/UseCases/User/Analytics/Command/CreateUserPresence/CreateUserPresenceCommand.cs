using MediatR;

namespace SFIDWebAPI.Application.UseCases.User.Analytics.Command.CreateUserPresence
{
    public class CreateUserPresenceCommand : IRequest<CreateUserPresenceDto>
    {
        public CreateUserPresenceData Data { set; get; }
    }

    public class CreateUserPresenceData
    {
        public int UserId { set; get; }
        public int DealerId { set; get; }
        public int CityId { set; get; }
        public int JobPositionId { set; get; }
        public int DealerBranchId { set; get; }
        public bool IsNew { set; get; }
        public string Platform { set; get; }
    }
}
