using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.StaticContent.Queries.GetContent;
using SFIDWebAPI.Application.UseCases.Admin.StaticContent.Command.UpdateContent;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/contents")]
    public class StaticContentController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;
        
        public StaticContentController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }        

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetContentDto>> Show(string name)
        {
            return Ok(await Mediator.Send(new GetContentQuery { Name = name, AdminName = _authAdmin.Name }));
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateContentDto>> Update([FromBody] UpdateContentCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
