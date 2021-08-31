namespace SFIDWebAPI.Application.Interfaces.Authorization
{
    public interface IAuthUser
    {
        public int UserId { set; get; }
        public string SalesCode { set; get; }
        public string Name { set; get; }
    }
}
