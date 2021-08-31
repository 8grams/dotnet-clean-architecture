using System;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Command.CreateUser
{
    public class CreateUserCommand : BaseAdminQueryCommand, IRequest<CreateUserDto>
    {
        public CreateUserData Data { set; get; }
    }

    public class CreateUserData
    {
        public string SalesmanCode { set; get; }
        public string SalesmanName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Password { set; get; }
        public string PasswordConfirmation { set; get; }
        public DateTime JoinDate { set; get; }
        public string SupervisorName { set; get; }
        public int JobPositionId { set; get; }
        public int PositionMetaId { set; get; }
        public int LevelId { set; get; }
        public int LastYearGrade { set; get; }
        public int CurrentYearGrade { set; get; }
        public int DealerBranchId { set; get; }
        public bool IsActive { set; get; }
    }
}
