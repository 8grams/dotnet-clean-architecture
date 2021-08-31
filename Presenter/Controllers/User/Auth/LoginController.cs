using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.Auth.Command.Login;

namespace SFIDWebAPI.Presenter.Controllers.User.Auth
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/auth/login")]
    public class LoginController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<LoginDto>> Login([FromBody] LoginCommand Payload)
        {
            return Ok(await Mediator.Send(Payload));
        }
    }
}
