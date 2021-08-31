using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.Auth.Command.ForgotPassword;

namespace SFIDWebAPI.Presenter.Controllers.User.Auth
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/auth/forgot-password")]
    public class ForgotPasswordController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ForgotPasswordDto>> ForgotPassword([FromBody] ForgotPasswordCommand Payload)
        {
            return Ok(await Mediator.Send(Payload));
        }
    }
}
