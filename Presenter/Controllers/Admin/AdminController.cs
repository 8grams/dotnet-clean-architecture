using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Queries.GetAdmins;
using SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.CreateAdmin;
using SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.UpdateAdmin;
using SFIDWebAPI.Application.UseCases.Admin.AdminProfile.Command.DeleteAdmin;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/admins")]
    public class AdminController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;
        
        public AdminController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }        

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetAdminsDto>> Index(
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType
            )
        {
            var Query = new GetAdminsQuery
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateAdminDto>> Store([FromBody] CreateAdminCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UpdateAdminDto>> Update([FromBody] UpdateAdminCommand Payload, int Id)
        {
            Payload.Data.Id = Id;
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }

        [HttpPost]
        [Route("/admin/admins/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteAdminDto>> Destroy([FromBody] DeleteAdminCommand Payload)
        {
            Payload.AdminName = _authAdmin.Name;
            return Ok(await Mediator.Send(Payload));
        }
    }
}
