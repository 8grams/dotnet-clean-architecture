using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.User.Bulletin.Queries.GetBulletinDetail;
using SFIDWebAPI.Application.UseCases.User.Bulletin.Queries.GetBulletinList;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/bulletins")]
    public class BulletinController : BaseController
    {
        private readonly IAuthUser _authUser;
        
        public BulletinController(IAuthUser authUser)
        {
            _authUser = authUser;
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BulletinListDto>> GetAll(
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType,
            [FromQuery(Name = "filter[year]")] string _FilterYear
            )
        {
            var _Filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterYear))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CreateYear",
                    Value = _FilterYear
                });
            }

            var Query = new BulletinListQuery
            {
                PagingPage = _PagingPage,
                PagingLimit = _PagingLimit,
                SortColumn = _SortColumn,
                SortType = _SortType,
                QuerySearch = _QuerySearch,
                Filters = _Filters,
                UserId = _authUser.UserId
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BulletinDetailDto>> GetById(int id)
        {
            return Ok(await Mediator.Send(new BulletinDetailQuery { Id = id, UserId = _authUser.UserId }));
        }
    }
}
