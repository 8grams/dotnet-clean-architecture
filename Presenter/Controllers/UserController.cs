using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.UseCases.User.Queries.GetUsers;
using WebApi.Application.UseCases.User.Queries.GetUser;
using WebApi.Application.UseCases.User.Command.CreateUser;
using WebApi.Application.UseCases.User.Command.UpdateUser;
using WebApi.Application.UseCases.User.Command.DeleteUser;
using WebApi.Application.Interfaces.Authorization;

namespace WebApi.Presenter.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/users")]
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
            [FromQuery(Name = "sort[type]")] string _SortType
            )
        {
            var Query = new GetUsersQuery
            {
                PagingPage = _PagingPage,
                PagingLimit = _PagingLimit,
                SortColumn = _SortColumn,
                SortType = _SortType,
                QuerySearch = _QuerySearch
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetUserDto>> Show(int id)
        {
            return Ok(await Mediator.Send(new GetUserQuery { 
                Id = id
            }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateUserDto>> Store([FromBody] CreateUserCommand Payload)
        {
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateUserDto>> Update([FromBody] UpdateUserCommand Payload, int Id)
        {
            Payload.Data.Id = Id;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteUserDto>> Destroy([FromBody] DeleteUserCommand Payload)
        {
            return Ok(await Mediator.Send(Payload));
        }
    }
}
