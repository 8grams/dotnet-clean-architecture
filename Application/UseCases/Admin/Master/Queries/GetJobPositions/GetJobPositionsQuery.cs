using MediatR;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetJobPositions
{
    public class GetJobPositionsQuery : IRequest<GetJobPositionsDto>
    {
        public string QuerySearch { set; get; }
    }
}
