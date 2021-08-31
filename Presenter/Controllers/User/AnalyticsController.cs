using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.Analytics.Command.CreateUserPresence;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/analytics")]
    public class AnalyticsController : BaseController
    {
        private readonly IAuthUser _authUser;
        
        public AnalyticsController(IAuthUser authUser)
        {
            _authUser = authUser;
        }
        
        [HttpPost]
        [Route("/analytics/presence")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateUserPresenceDto>> Store([FromBody] CreateUserPresenceCommand Payload)
        {
            return Ok(await Mediator.Send(Payload));
        }
    }
}
