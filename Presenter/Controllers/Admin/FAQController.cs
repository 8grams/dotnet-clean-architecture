using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.FAQ.Queries.GetFAQs;
using SFIDWebAPI.Application.UseCases.Admin.FAQ.Queries.GetFAQ;
using SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.CreateFAQ;
using SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.UpdateFAQ;
using SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.DeleteFAQ;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/faqs")]
    public class FAQController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;

        public FAQController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        } 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetFAQsDto>> Index(
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType
            )
        {
            var Query = new GetFAQsQuery
            {
                PagingPage = _PagingPage,
                PagingLimit = _PagingLimit,
                SortColumn = _SortColumn,
                SortType = _SortType,
                QuerySearch = _QuerySearch,
                AdminName = _authAdmin.Name
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetFAQDto>> Show(int id)
        {
            return Ok(await Mediator.Send(new GetFAQQuery { Id = id, AdminName = _authAdmin.Name }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateFAQDto>> Store([FromBody] CreateFAQCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateFAQDto>> Update([FromBody] UpdateFAQCommand Payload, int Id)
        {
            Payload.Data.Id = Id;
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/admin/faqs/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteFAQDto>> Destroy([FromBody] DeleteFAQCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
