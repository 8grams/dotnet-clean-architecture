using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Infrastructure.Authorization
{
    public class AuthAdmin : IAuthAdmin
    {
        public string Name { set; get; }
    }
}
