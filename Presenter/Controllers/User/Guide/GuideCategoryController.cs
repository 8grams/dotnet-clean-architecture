using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SFIDWebAPI.Application.UseCases.User.Guide.Category.Queries.GetGuideCategoryList;
using SFIDWebAPI.Application.UseCases.User.Guide.Category.Queries.GetGuideCategoryDetail;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User.Guide
{
	[ApiController]
	[Produces(MediaTypeNames.Application.Json)]
	[Route("/guide-categories")]
	public class GuideCategoryController : BaseController
	{
		private readonly IAuthUser _authUser;
        
        public GuideCategoryController(IAuthUser authUser)
        {
            _authUser = authUser;
        } 
		
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<GuideCategoryListDto>> GetAll(
			[FromQuery(Name = "paging[page]")] int _PagingPage,
			[FromQuery(Name = "paging[limit]")] int _PagingLimit,
			[FromQuery(Name = "search")] string _QuerySearch,
			[FromQuery(Name = "sort[column]")] string _SortColumn,
			[FromQuery(Name = "sort[type]")] string _SortType,
			[FromQuery(Name = "includes")] string _Includes
			)
		{
			var Query = new GuideCategoryListQuery
            {
				PagingPage = _PagingPage,
				PagingLimit = _PagingLimit,
				SortColumn = _SortColumn,
				SortType = _SortType,
				QuerySearch = _QuerySearch,
				Includes = new List<string>(),
				UserId = _authUser.UserId
			};

			if (!string.IsNullOrEmpty(_Includes)) 
            {
                Query.Includes = _Includes.Split(',').ToList<string>();
            }
			
			return Ok(await Mediator.Send(Query));
		}

		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<GuideCategoryDetailDto>> GetById(int id)
		{
			return Ok(await Mediator.Send(new GuideCategoryDetailQuery { Id = id, UserId = _authUser.UserId }));
		}
	}
}
