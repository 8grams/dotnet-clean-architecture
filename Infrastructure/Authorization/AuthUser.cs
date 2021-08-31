using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Infrastructure.Authorization
{
    public class AuthUser : IAuthUser
    {
        public int UserId { set; get; }
        public string SalesCode { set; get; }
        public string Name { set; get; }
    }
}
