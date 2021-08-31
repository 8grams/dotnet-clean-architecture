using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.FAQ.Queries.GetFAQList;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/faqs")]
    public class FAQController : BaseController
    {
        private readonly IAuthUser _authUser;
        
        public FAQController(IAuthUser authUser)
        {
            _authUser = authUser;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetFAQListDto>> GetAll()
        {
            return Ok(await Mediator.Send(new GetFAQListQuery() { UserId = _authUser.UserId }));
        }
    }
}
