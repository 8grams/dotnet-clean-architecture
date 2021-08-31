using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Queries.GetGuideMaterials;
using SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Queries.GetGuideMaterial;
using SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.CreateGuideMaterial;
using SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.UpdateGuideMaterial;
using SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.DeleteGuideMaterial;
using SFIDWebAPI.Application.Interfaces.Authorization;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/guide-materials")]
    public class GuideMaterialController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;

        public GuideMaterialController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetGuideMaterialsDto>> Index(
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType,
            [FromQuery(Name = "filter[category]")] string _FilterCategory
            )
        {

            var filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterCategory))
            {
                filters.Add(new FilterParams()
                {
                    Column = "MasterCarId",
                    Value = _FilterCategory
                });
            }

            var Query = new GetGuideMaterialsQuery
            {
                PagingPage = _PagingPage,
                PagingLimit = _PagingLimit,
                SortColumn = _SortColumn,
                SortType = _SortType,
                QuerySearch = _QuerySearch,
                AdminName = _authAdmin.Name,
                Filters = filters
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetGuideMaterialDto>> Show(int id)
        {
            return Ok(await Mediator.Send(new GetGuideMaterialQuery { Id = id, AdminName = _authAdmin.Name }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateGuideMaterialDto>> Store([FromBody] CreateGuideMaterialCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateGuideMaterialDto>> Update([FromBody] UpdateGuideMaterialCommand Payload, int Id)
        {
            Payload.Data.Id = Id;
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/admin/guide-materials/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteGuideMaterialDto>> Destroy([FromBody] DeleteGuideMaterialCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
