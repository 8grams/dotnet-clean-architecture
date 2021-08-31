using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.Notification.Queries.GetNotifications;
using SFIDWebAPI.Application.UseCases.Admin.Notification.Queries.GetNotification;
using SFIDWebAPI.Application.UseCases.Admin.Notification.Command.CreateNotification;
using SFIDWebAPI.Application.UseCases.Admin.Notification.Command.UpdateNotification;
using SFIDWebAPI.Application.UseCases.Admin.Notification.Command.DeleteNotification;
using SFIDWebAPI.Application.Interfaces.Authorization;


namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/notifications")]
    public class NotificationController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;

        public NotificationController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetNotificationsDto>> Index(
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType
            )
        {
            var Query = new GetNotificationsQuery
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
        public async Task<ActionResult<GetNotificationDto>> Show(int id)
        {
            return Ok(await Mediator.Send(new GetNotificationQuery { Id = id, AdminName = _authAdmin.Name }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateNotificationDto>> Store([FromBody] CreateNotificationCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateNotificationDto>> Update([FromBody] UpdateNotificationCommand Payload, int Id)
        {
            Payload.Data.Id = Id;
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/admin/notifications/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteNotificationDto>> Destroy([FromBody] DeleteNotificationCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
