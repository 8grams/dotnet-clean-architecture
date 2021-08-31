using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Queries.GetHomeBanners;
using SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Queries.GetHomeBanner;
using SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.CreateHomeBanner;
using SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.UpdateHomeBanner;
using SFIDWebAPI.Application.UseCases.Admin.HomeBanner.Command.DeleteHomeBanner;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/banners")]
    public class HomeBannerController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;

        public HomeBannerController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetHomeBannersDto>> Index(
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType
            )
        {
            var Query = new GetHomeBannersQuery
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
        public async Task<ActionResult<GetHomeBannerDto>> Show(int id)
        {
            return Ok(await Mediator.Send(new GetHomeBannerQuery { Id = id, AdminName = _authAdmin.Name }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateHomeBannerDto>> Store([FromBody] CreateHomeBannerCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateHomeBannerDto>> Update([FromBody] UpdateHomeBannerCommand Payload, int Id)
        {
            Payload.Data.Id = Id;
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/admin/banners/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteHomeBannerDto>> Destroy([FromBody] DeleteHomeBannerCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
