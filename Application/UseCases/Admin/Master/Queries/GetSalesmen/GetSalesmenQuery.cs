using MediatR;

namespace SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetSalesmen
{
    public class GetSalesmenQuery : IRequest<GetSalesmenDto>
    {
        public string QuerySearch { set; get; }
    }
}
