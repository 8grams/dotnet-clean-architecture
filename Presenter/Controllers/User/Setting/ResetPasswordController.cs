using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.Setting.Command.ResetPassword;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User.Setting
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/setting/reset-password")]
    public class ResetPasswordController : BaseController
    {
        private readonly IAuthUser _authUser;
        
        public ResetPasswordController(IAuthUser authUser)
        {
            _authUser = authUser;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResetPasswordDto>> Register([FromBody] ResetPasswordCommand Payload)
        {
            Payload.UserId = _authUser.UserId;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
