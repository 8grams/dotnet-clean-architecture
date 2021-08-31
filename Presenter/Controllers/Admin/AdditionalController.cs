using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Queries.GetInfos;
using SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Queries.GetInfo;
using SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Command.CreateInfo;
using SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Command.UpdateInfo;
using SFIDWebAPI.Application.UseCases.Admin.AdditionalInfo.Command.DeleteInfo;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/infos")]
    public class AdditionalInfoController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;
        
        public AdditionalInfoController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        } 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetInfosDto>> Index(
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType
            )
        {
            var Query = new GetInfosQuery
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
        public async Task<ActionResult<GetInfoDto>> Show(int id)
        {
            return Ok(await Mediator.Send(new GetInfoQuery { 
                Id = id,
                AdminName = _authAdmin.Name
            }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateInfoDto>> Store([FromBody] CreateInfoCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateInfoDto>> Update([FromBody] UpdateInfoCommand Payload, int Id)
        {
            Payload.Data.Id = Id;
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/admin/infos/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteInfoDto>> Destroy([FromBody] DeleteInfoCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
