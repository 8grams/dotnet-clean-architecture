using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.Auth.Command.Register;

namespace SFIDWebAPI.Presenter.Controllers.User.Auth
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/auth/register")]
    public class RegisterController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RegisterDto>> Register([FromBody] RegisterCommand Payload)
        {
            return Ok(await Mediator.Send(Payload));
        }
    }
}
