using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.User.StaticContent.Queries.GetStaticContent;

namespace SFIDWebAPI.Presenter.Controllers.User
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/static")]
    public class StaticContentController : BaseController
    {

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StaticContentDto>> GetByName(string name)
        {
            return Ok(await Mediator.Send(new StaticContentQuery { Name = name }));
        }
    }
}
