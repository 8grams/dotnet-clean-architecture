using System.Net.Mime;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.Training.Material.Queries.GetTrainingMaterials;
using SFIDWebAPI.Application.UseCases.Admin.Training.Material.Queries.GetTrainingMaterial;
using SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.CreateTrainingMaterial;
using SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.UpdateTrainingMaterial;
using SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.DeleteTrainingMaterial;
using SFIDWebAPI.Application.Interfaces.Authorization;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/training-materials")]
    public class TrainingMaterialController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;

        public TrainingMaterialController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetTrainingMaterialsDto>> Index(
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

            var Query = new GetTrainingMaterialsQuery
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
        public async Task<ActionResult<GetTrainingMaterialDto>> Show(int id)
        {
            return Ok(await Mediator.Send(new GetTrainingMaterialQuery { Id = id, AdminName = _authAdmin.Name }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateTrainingMaterialDto>> Store([FromBody] CreateTrainingMaterialCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateTrainingMaterialDto>> Update([FromBody] UpdateTrainingMaterialCommand Payload, int Id)
        {
            Payload.Data.Id = Id;
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/admin/training-materials/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteTrainingMaterialDto>> Destroy([FromBody] DeleteTrainingMaterialCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
