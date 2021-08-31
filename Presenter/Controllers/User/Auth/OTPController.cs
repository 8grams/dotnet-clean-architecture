using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.Auth.Command.OTP;
using SFIDWebAPI.Application.UseCases.User.Auth.Command.ResendOTP;
using SFIDWebAPI.Application.Models.Query;

namespace SFIDWebAPI.Presenter.Controllers.User.Auth
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class OTPController : BaseController
    {
        [HttpPost]
        [Route("/auth/otp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<OTPDto>> GetOTP([FromBody] OTPCommand Payload)
        {
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/auth/resend-otp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ResendOTPDto>> ResendOTP([FromBody] ResendOTPCommand Payload)
        {
            return Ok(await Mediator.Send(Payload));
        }
    }
}
