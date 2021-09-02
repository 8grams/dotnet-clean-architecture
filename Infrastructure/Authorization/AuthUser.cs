using WebApi.Application.Interfaces.Authorization;

namespace WebApi.Infrastructure.Authorization
{
    public class AuthUser : IAuthUser
    {
        public int UserId { set; get; }
        public string Name { set; get; }
    }
}
