using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.Auth.Command.Login;

namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthController : BaseController
    {
        [Route("/admin/auth/login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<LoginDto>> Store([FromBody] LoginCommand Payload)
        {
            return Ok(await Mediator.Send(Payload));
        }  
    }
}
