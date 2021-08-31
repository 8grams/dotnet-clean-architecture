using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.UpdateAdmin
{
    public class UpdateAdminCommand : BaseAdminQueryCommand, IRequest<UpdateAdminDto>
    {
        public UpdateAdminData Data { set; get; }
    }

    public class UpdateAdminData
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public int RoleId { set; get; }    }
}
