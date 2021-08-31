using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Queries.GetImageGalleries;
using SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Queries.GetImageGallery;
using SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.CreateImageGallery;
using SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.UpdateImageGallery;
using SFIDWebAPI.Application.UseCases.Admin.ImageGallery.Command.DeleteImageGallery;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/galleries")]
    public class ImageGalleryController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;
        
        public ImageGalleryController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }        

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetImageGalleriesDto>> Index(
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType
            )
        {
            var Query = new GetImageGalleriesQuery
            {
                PagingPage = _PagingPage,
                PagingLimit = _PagingLimit,
                SortColumn = _SortColumn,
                SortType = _SortType,
                QuerySearch = _QuerySearch,
                AdminName = _authAdmin.Name
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetImageGalleryDto>> Show(int id)
        {
            return Ok(await Mediator.Send(new GetImageGalleryQuery { Id = id, AdminName = _authAdmin.Name }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateImageGalleryDto>> Store([FromBody] CreateImageGalleryCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateImageGalleryDto>> Update([FromBody] UpdateImageGalleryCommand Payload, int Id)
        {
            Payload.Data.Id = Id;
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/admin/galleries/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteImageGalleryDto>> Destroy([FromBody] DeleteImageGalleryCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
