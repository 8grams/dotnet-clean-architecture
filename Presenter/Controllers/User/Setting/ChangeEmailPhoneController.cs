using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.Setting.Command.ChangeEmailPhone;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User.Setting
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/setting/change-email-phone")]
    public class ChangeEmailPhoneController : BaseController
    {
        private readonly IAuthUser _authUser;
        
        public ChangeEmailPhoneController(IAuthUser authUser)
        {
            _authUser = authUser;
        }


        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ChangeEmailPhoneDto>> Update([FromBody] ChangeEmailPhoneCommand Payload)
        {
            Payload.UserId = _authUser.UserId;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
