using MediatR;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetCities
{
    public class GetCitiesQuery : IRequest<GetCitiesDto>
    {
        public string QuerySearch { set; get; }
    }
}
