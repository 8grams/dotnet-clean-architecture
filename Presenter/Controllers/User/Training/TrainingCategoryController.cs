using System.Threading.Tasks;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SFIDWebAPI.Application.UseCases.User.Training.Category.Queries.GetTrainingCategoryList;
using SFIDWebAPI.Application.UseCases.User.Training.Category.Queries.GetTrainingCategoryDetail;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User.Training
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/training-categories")]
    public class TrainingCategoryController : BaseController
    {
        private readonly IAuthUser _authUser;
        
        public TrainingCategoryController(IAuthUser authUser)
        {
            _authUser = authUser;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TrainingCategoryListDto>> GetAll(
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "search")] string _QuerySearch,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType
            )
        {
            var Query = new TrainingCategoryListQuery()
            {
                PagingPage = _PagingPage,
                PagingLimit = _PagingLimit,
                SortColumn = _SortColumn,
                SortType = _SortType,
                QuerySearch = _QuerySearch,
                UserId = _authUser.UserId
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrainingCategoryDetailDto>> GetById(int id)
        {
            return Ok(await Mediator.Send(new TrainingCategoryDetailQuery { Id = id, UserId = _authUser.UserId }));
        }
    }
}
