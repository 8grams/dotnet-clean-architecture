using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.Recommendation.Queries.GetRecommendations;
using SFIDWebAPI.Application.UseCases.Admin.Recommendation.Queries.GetRecommendation;
using SFIDWebAPI.Application.UseCases.Admin.Recommendation.Command.CreateRecommendation;
using SFIDWebAPI.Application.UseCases.Admin.Recommendation.Command.DeleteRecommendation;
using SFIDWebAPI.Application.Interfaces.Authorization;


namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/recommendations")]
    public class RecommendationController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;

        public RecommendationController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetRecommendationDto>> Index()
        {

            return Ok(await Mediator.Send(new GetRecommendationsQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetRecommendationQuery>> Show(
            [FromQuery(Name = "content_id")] int _ContentId,
            [FromQuery(Name = "content_type")] string _ContentType
        )
        {
            return Ok(await Mediator.Send(new GetRecommendationQuery { 
                ContentId = _ContentId, 
                ContentType = _ContentType, 
                AdminName = _authAdmin.Name }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateRecommendationDto>> Store([FromBody] CreateRecommendationCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/admin/recommendations/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteRecommendationDto>> Destroy([FromBody] DeleteRecommendationCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
