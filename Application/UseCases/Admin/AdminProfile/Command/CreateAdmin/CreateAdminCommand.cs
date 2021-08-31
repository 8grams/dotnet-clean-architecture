using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.CreateAdmin
{
    public class CreateAdminCommand : BaseAdminQueryCommand, IRequest<CreateAdminDto>
    {
        public CreateAdminData Data { set; get; }
    }

    public class CreateAdminData
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Phone { set; get; }
        public int RoleId { set; get; }    
    }
}
