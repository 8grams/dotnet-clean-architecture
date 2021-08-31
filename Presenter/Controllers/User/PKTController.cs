
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SFIDWebAPI.Application.UseCases.User.PKT.History.Queries.GetHistoryList;
using SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.SendReport;
using SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.VerifyVin;
using SFIDWebAPI.Application.UseCases.User.PKT.Report.Command.DeleteReport;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class PKTController : BaseController
    {
        private readonly IAuthUser _authUser;
        
        public PKTController(IAuthUser authUser)
        {
            _authUser = authUser;
        }

        [HttpPost]
        [Route("/pkt/verify-vin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VerifyVinDto>> VerifyVin([FromBody] VerifyVinCommand Payload)
        {
            Payload.UserId = _authUser.UserId;
            Payload.Data.SalesCode = _authUser.SalesCode;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/pkt/report")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SendReportDto>> VerifyVin([FromBody] SendReportCommand Payload)
        {
            Payload.UserId = _authUser.UserId;
            Payload.Data.SalesCode = _authUser.SalesCode;
            return Ok(await Mediator.Send(Payload));
        }
        
        [HttpGet]
        [Route("/pkt/history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PKTHistoryListDto>> GetHistories(
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType
            )
        {

            var Query = new PKTHistoryListQuery
            {
                PagingPage = _PagingPage,
                PagingLimit = _PagingLimit,
                SortColumn = _SortColumn,
                SortType = _SortType,
                QuerySearch = _QuerySearch,
                UserId = _authUser.UserId,
                SalesCode = _authUser.SalesCode
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpPost]
        [Route("/pkt/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteReportDto>> Destroy([FromBody] DeleteReportCommand Payload)
        {
            return Ok(await Mediator.Send(Payload));
        }
    }
}
