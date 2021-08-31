using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.Auth.Command.VerifyUser;

namespace SFIDWebAPI.Presenter.Controllers.User.Auth
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/auth/verify")]
    public class VerifyUserController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VerifyUserDto>> Register([FromBody] VerifyUserCommand Payload)
        {
            return Ok(await Mediator.Send(Payload));
        }
    }
}
