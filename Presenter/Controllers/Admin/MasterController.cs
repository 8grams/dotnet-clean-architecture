using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetCities;
using SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealers;
using SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealerBranches;
using SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetDealerGroups;
using SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetJobPositions;
using SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetSalesmanLevels;
using SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetPositionMetas;
using SFIDWebAPI.Application.UseCases.Admin.Master.Queries.GetSalesmen;
using SFIDWebAPI.Application.UseCases.Admin.Master.Command.CreatePositionMeta;
using SFIDWebAPI.Application.UseCases.Admin.Master.Command.UpdatePositionMeta;
using SFIDWebAPI.Application.UseCases.Admin.Master.Command.DeletePositionMeta;
using SFIDWebAPI.Application.Interfaces.Authorization;


namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/master")]
    public class MasterController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;

        public MasterController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }

        [HttpGet]
        [Route("/admin/master/city")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetCitiesDto>> GetAllCities(
            [FromQuery(Name = "search")] string _QuerySearch
            )
        {
            var Query = new GetCitiesQuery
            {
                QuerySearch = _QuerySearch
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/master/dealer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetDealersDto>> GetAllDealers(
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "filter[city]")] string _FilterCity
            )
        {
            var _Filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterCity))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CityId",
                    Value = _FilterCity
                });
            }

            var Query = new GetDealersQuery
            {
                QuerySearch = _QuerySearch,
                Filters = _Filters
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/master/branch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetDealerBranchesDto>> GetAllDealerBranches(
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "filter[dealer]")] string _FilterDealer
            )
        {
            var _Filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterDealer))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerId",
                    Value = _FilterDealer
                });
            }

            var Query = new GetDealerBranchesQuery
            {
                QuerySearch = _QuerySearch,
                Filters = _Filters
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/master/group")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetDealerGroupsDto>> GetAllDealerGroups(
            [FromQuery(Name = "search")] string _QuerySearch
            )
        {
            var Query = new GetDealerGroupsQuery
            {
                QuerySearch = _QuerySearch
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/master/job-positions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetJobPositionsDto>> GetAllJobPosition(
            [FromQuery(Name = "search")] string _QuerySearch
            )
        {
            var Query = new GetJobPositionsQuery
            {
                QuerySearch = _QuerySearch
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/master/salesman-levels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetSalesmanLevelsDto>> GetAllSalesmanLevels()
        {
            var Query = new GetSalesmanLevelsQuery
            {
                
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/master/salesmen")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetSalesmenDto>> GetAllSalesmen(
            [FromQuery(Name = "search")] string _QuerySearch
        )
        {
            var Query = new GetSalesmenQuery
            {
                QuerySearch = _QuerySearch
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/master/sfd-positions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetPositionMetasDto>> GetPositionMetas(
            )
        {
            var Query = new GetPositionMetasQuery
            {
                AdminName = _authAdmin.Name
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpPost]
        [Route("/admin/master/sfd-positions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreatePositionMetaDto>> StorePositionMeta([FromBody] CreatePositionMetaCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch]
        [Route("/admin/master/sfd-positions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdatePositionMetaDto>> UpdatePositionMeta([FromBody] UpdatePositionMetaCommand Payload, int Id)
        {
            Payload.AdminName = _authAdmin.Name;
            Payload.Data.Id = Id;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/admin/master/sfd-positions/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeletePositionMetaDto>> DestroyPositionMeta([FromBody] DeletePositionMetaCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
