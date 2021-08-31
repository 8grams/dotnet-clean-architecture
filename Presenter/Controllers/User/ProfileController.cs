using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SFIDWebAPI.Application.UseCases.User.UserProfile.Queries.GetProfile;
using SFIDWebAPI.Application.UseCases.User.UserProfile.Queries.CheckNotification;
using SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UpdateProfile;
using SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UploadPhoto;
using SFIDWebAPI.Application.UseCases.User.UserProfile.Command.UpdateFirebaseToken;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/me")]
    public class ProfileController : BaseController
    {
        private readonly IAuthUser _authUser;

        public ProfileController(IAuthUser authUser)
        {
            _authUser = authUser;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetProfileDto>> Show()
        {
            var Query = new GetProfileQuery
            {
                Id = _authUser.UserId
            };
            return Ok(await Mediator.Send(Query));
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateProfileDto>> Update([FromBody] UpdateProfileCommand Payload)
        {
            Payload.UserId = _authUser.UserId;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch]
        [Route("/me/upload-photo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UploadPhotoDto>> UploadPhoto([FromBody] UploadPhotoCommand Payload)
        {
            Payload.UserId = _authUser.UserId;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch]
        [Route("/me/firebase-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateFirebaseTokenDto>> UploadFirebaseToken([FromBody] UpdateFirebaseTokenCommand Payload)
        {
            Payload.UserId = _authUser.UserId;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpGet]
        [Route("/me/check-notification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CheckNotificationDto>> Check()
        {
            var Query = new CheckNotificationQuery
            {
                UserId = _authUser.UserId
            };
            return Ok(await Mediator.Send(Query));
        }
    }
}
