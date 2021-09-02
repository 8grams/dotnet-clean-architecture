namespace WebApi.Application.Interfaces.Authorization
{
    public interface IAuthUser
    {
        public int UserId { set; get; }
        public string Name { set; get; }
    }
}
