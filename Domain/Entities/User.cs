using System;
using WebApi.Domain.Infrastructures;

namespace WebApi.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { set; get; }
        public string UserName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public int Age { set; get; }
        public string ProfilePicture { set; get; }
    }
}
