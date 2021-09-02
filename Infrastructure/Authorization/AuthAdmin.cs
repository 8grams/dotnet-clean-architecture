using WebApi.Application.Interfaces.Authorization;

namespace WebApi.Infrastructure.Authorization
{
    public class AuthAdmin : IAuthAdmin
    {
        public string Name { set; get; }
    }
}
