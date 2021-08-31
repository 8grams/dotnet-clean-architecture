using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.UseCases.User.Guide.Material.Queries.GetGuideMaterialDetail;
using SFIDWebAPI.Application.UseCases.User.Guide.Material.Queries.GetGuideMaterialList;
using SFIDWebAPI.Application.Interfaces.Authorization;

namespace SFIDWebAPI.Presenter.Controllers.User.Guide
{
	[ApiController]
	[Produces(MediaTypeNames.Application.Json)]
	[Route("/guide-materials")]
	public class GuideMaterialController : BaseController
	{
		private readonly IAuthUser _authUser;
        
        public GuideMaterialController(IAuthUser authUser)
        {
            _authUser = authUser;
        } 

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<GuideMaterialListDto>> GetAll(
			[FromQuery(Name = "paging[page]")] int _PagingPage,
			[FromQuery(Name = "paging[limit]")] int _PagingLimit,
			[FromQuery(Name = "search")] string _QuerySearch,
			[FromQuery(Name = "filter[category]")] string _FilterCategory,
			[FromQuery(Name = "sort[column]")] string _SortColumn,
			[FromQuery(Name = "sort[type]")] string _SortType,
			[FromQuery(Name = "includes")] string _Includes
			)
		{
			var _Filters = new List<FilterParams>();
			if (!string.IsNullOrEmpty(_FilterCategory))
            {
                _Filters.Add(new FilterParams
                {
					Column = "MasterCarId",
                    Value = _FilterCategory
                });
            }

			var Query = new GuideMaterialListQuery
            {
				PagingPage = _PagingPage,
				PagingLimit = _PagingLimit,
				SortColumn = _SortColumn,
				SortType = _SortType,
				Filters = _Filters,
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
		public async Task<ActionResult<GuideMaterialDetailDto>> GetById(int id)
		{
			return Ok(await Mediator.Send(new GuideMaterialDetailQuery { Id = id, UserId = _authUser.UserId }));
		}
	}
}
