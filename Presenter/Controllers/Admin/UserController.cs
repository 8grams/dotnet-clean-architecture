using System.Net.Mime;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.User.Queries.GetUsers;
using SFIDWebAPI.Application.UseCases.Admin.User.Queries.GetUser;
using SFIDWebAPI.Application.UseCases.Admin.User.Command.CreateUser;
using SFIDWebAPI.Application.UseCases.Admin.User.Command.UpdateUser;
using SFIDWebAPI.Application.UseCases.Admin.User.Command.DeleteUser;
using SFIDWebAPI.Application.UseCases.Admin.User.Command.ImportUser;
using SFIDWebAPI.Application.UseCases.Admin.User.Queries.ExportUser;
using SFIDWebAPI.Application.Interfaces.Authorization;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/users")]

    public class UserController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;
        public UserController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetUsersDto>> Index(
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType,
            [FromQuery(Name = "filter[position]")] string _FilterPosition,
            [FromQuery(Name = "filter[dealerName]")] string _FilterDealerName,
            [FromQuery(Name = "filter[dealerCity]")] string _FilterDealerCity,
            [FromQuery(Name = "filter[dealerBranch]")] string _FilterDealerBranch,
            [FromQuery(Name = "filter[active]")] string _FilterActive,
            [FromQuery(Name = "filter[type]")] string _FilterType
        )
        {
            var filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterPosition))
            {
                filters.Add(new FilterParams()
                {
                    Column = "JobPosition",
                    Value = _FilterPosition
                });
            }

            if (!string.IsNullOrEmpty(_FilterDealerName))
            {
                filters.Add(new FilterParams()
                {
                    Column = "DealerName",
                    Value = _FilterDealerName
                });
     
            }

            if (!string.IsNullOrEmpty(_FilterDealerCity))
            {
                filters.Add(new FilterParams()
                {
                    Column = "DealerCity",
                    Value = _FilterDealerCity
                });
     
            }

            if (!string.IsNullOrEmpty(_FilterDealerBranch))
            {
                filters.Add(new FilterParams()
                {
                    Column = "DealerBranch",
                    Value = _FilterDealerBranch
                });
     
            }

            if (!string.IsNullOrEmpty(_FilterActive))
            {
                filters.Add(new FilterParams()
                {
                    Column = "IsActive",
                    Value = _FilterActive
                });
     
            }

            var Query = new GetUsersQuery
            {
                PagingPage = _PagingPage,
                PagingLimit = _PagingLimit,
                SortColumn = _SortColumn,
                SortType = _SortType,
                QuerySearch = _QuerySearch,
                AdminName = _authAdmin.Name,
                Filters = filters,
                Type = _FilterType
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetUserDto>> Show(int id)
        {
            return Ok(await Mediator.Send(new GetUserQuery { Id = id, AdminName = _authAdmin.Name }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateUserDto>> Store([FromBody] CreateUserCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpGet]
        [Route("/admin/users/export")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ExportUserDto>> Export(
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "filter[position]")] string _FilterPosition,
            [FromQuery(Name = "filter[dealerName]")] string _FilterDealerName,
            [FromQuery(Name = "filter[dealerCity]")] string _FilterDealerCity,
            [FromQuery(Name = "filter[dealerBranch]")] string _FilterDealerBranch,
            [FromQuery(Name = "filter[active]")] string _FilterIsActive,
            [FromQuery(Name = "filter[type]")] string _FilterType
        )
        {
            var filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterPosition))
            {
                filters.Add(new FilterParams()
                {
                    Column = "JobPosition",
                    Value = _FilterPosition
                });
     
            }

            if (!string.IsNullOrEmpty(_FilterDealerBranch))
            {
                filters.Add(new FilterParams()
                {
                    Column = "DealerBranch",
                    Value = _FilterDealerBranch
                });
     
            }

            if (!string.IsNullOrEmpty(_FilterIsActive))
            {
                filters.Add(new FilterParams()
                {
                    Column = "IsActive",
                    Value = _FilterIsActive
                });
     
            }

            if (!string.IsNullOrEmpty(_FilterDealerName))
            {
                filters.Add(new FilterParams()
                {
                    Column = "DealerName",
                    Value = _FilterDealerName
                });
     
            }

            if (!string.IsNullOrEmpty(_FilterDealerCity))
            {
                filters.Add(new FilterParams()
                {
                    Column = "DealerCity",
                    Value = _FilterDealerCity
                });
     
            }

            if (!string.IsNullOrEmpty(_FilterType))
            {
                filters.Add(new FilterParams()
                {
                    Column = "Type",
                    Value = _FilterType
                });
     
            }

            var Query = new ExportUserQuery
            {
                QuerySearch = _QuerySearch,
                Filters = filters
            };
            return Ok(await Mediator.Send(Query));
        }

        [HttpPost]
        [Route("/admin/users/import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ImportUserDto>> Store([FromBody] ImportUserCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateUserDto>> Update([FromBody] UpdateUserCommand Payload, int Id)
        {
            Payload.Data.Id = Id;
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/admin/users/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteUserDto>> Destroy([FromBody] DeleteUserCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}