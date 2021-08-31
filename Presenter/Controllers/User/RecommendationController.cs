using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.Recommendation.Queries.GetRecommendationList;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/recommendations")]
    public class RecommendationController : BaseController
    {
        private readonly IAuthUser _authUser;
        
        public RecommendationController(IAuthUser authUser)
        {
            _authUser = authUser;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RecommendationListDto>> GetAll()
        {
            var Query = new RecommendationListQuery() { 
                UserId = _authUser.UserId
            };
            return Ok(await Mediator.Send(Query));
        }
    }
}
