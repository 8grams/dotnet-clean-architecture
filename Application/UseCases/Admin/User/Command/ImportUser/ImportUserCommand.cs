using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Command.ImportUser
{
    public class ImportUserCommand : BaseAdminQueryCommand, IRequest<ImportUserDto>
    {
        public ImportUserData Data { set; get; }
    }

    public class ImportUserData
    {
        public string FileName { set; get; }
        public string FileByte { set; get; }
    }
}
