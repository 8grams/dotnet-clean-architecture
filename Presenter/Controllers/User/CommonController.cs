using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.Common.Command.UpdateCounter;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class CommonController : BaseController
    {
        private readonly IAuthUser _authUser;
        
        public CommonController(IAuthUser authUser)
        {
            _authUser = authUser;
        }

        [HttpPost]
        [Route("/counters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateCounterDto>> VerifyUser([FromBody] UpdateCounterCommand Payload)
        {
            Payload.UserId = _authUser.UserId;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
