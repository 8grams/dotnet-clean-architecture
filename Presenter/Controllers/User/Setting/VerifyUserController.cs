using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.Setting.Command.VerifyUser;
using SFIDWebAPI.Application.UseCases.User.Setting.Command.OTP;
using SFIDWebAPI.Application.UseCases.User.Setting.Command.OTP.Trigger;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User.Setting
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class VerifyUserController : BaseController
    {
        private readonly IAuthUser _authUser;
        
        public VerifyUserController(IAuthUser authUser)
        {
            _authUser = authUser;
        }

        [HttpPost]
        [Route("/setting/verify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<VerifyUserDto>> VerifyUser([FromBody] VerifyUserCommand Payload)
        {
            Payload.UserId = _authUser.UserId;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/setting/request-otp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OTPTriggerDto>> TriggerOTP([FromBody] OTPTriggerCommand Payload)
        {
            Payload.UserId = _authUser.UserId;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/setting/verify-otp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OTPDto>> VerifyOTP([FromBody] OTPCommand Payload)
        {
            Payload.UserId = _authUser.UserId;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
