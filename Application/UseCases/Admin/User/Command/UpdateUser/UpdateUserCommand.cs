using System;
using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Command.UpdateUser
{
    public class UpdateUserCommand : BaseAdminQueryCommand, IRequest<UpdateUserDto>
    {
        public UpdateUserData Data { set; get; }
    }

    public class UpdateUserData
    {
        public int Id { set; get; }
        public string SalesmanName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public DateTime JoinDate { set; get; }
        public string SupervisorName { set; get; }
        public int JobPositionId { set; get; }
        public int LevelId { set; get; }
        public int LastYearGrade { set; get; }
        public int CurrentYearGrade { set; get; }
        public int DealerBranchId { set; get; }
        public bool IsActive { set; get; }
    }
}
