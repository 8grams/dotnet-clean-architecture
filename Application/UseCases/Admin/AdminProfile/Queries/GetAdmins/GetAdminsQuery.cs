using MediatR;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Queries.GetAdmins
{
    public class GetAdminsQuery : AdminPaginationQuery, IRequest<GetAdminsDto>
    {
    }
}
