using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SFIDWebAPI.Application.Models.Query;
using SFIDWebAPI.Application.Interfaces.Authorization;
using SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Dashboard;
using SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Users;
using SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Bulletin;
using SFIDWebAPI.Application.UseCases.Admin.Report.Queries.Info;
using SFIDWebAPI.Application.UseCases.Admin.Report.Queries.TrainingMaterial;
using SFIDWebAPI.Application.UseCases.Admin.Report.Queries.QuickGuide;
using SFIDWebAPI.Application.UseCases.Admin.Report.Queries.ExportBulletin;
using SFIDWebAPI.Application.UseCases.Admin.Report.Queries.ExportInfo;
using SFIDWebAPI.Application.UseCases.Admin.Report.Queries.ExportQuickGuide;
using SFIDWebAPI.Application.UseCases.Admin.Report.Queries.ExportTrainingMaterial;

namespace SFIDWebAPI.Presenter.Controllers.Admin
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/admin/report")]
    public class ReportController : BaseController
    {
        private readonly IAuthAdmin _authAdmin;

        public ReportController(IAuthAdmin authAdmin)
        {
            _authAdmin = authAdmin;
        }

        [HttpGet]
        [Route("/admin/report/dashboard")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetDashboardDto>> GetDashboard()
        {

            var Query = new GetDashboardQuery
            {
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/report/user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetUsersReportDto>> GetUsersReport(
            [FromQuery(Name = "filter[salescode]")] string _FilterSalesCode,
            [FromQuery(Name = "filter[position]")] string _FilterPosition,
            [FromQuery(Name = "filter[dealer]")] string _FilterDealer,
            [FromQuery(Name = "filter[city]")] string _FilterCity,
            [FromQuery(Name = "filter[branch]")] string _FilterBranch,
            [FromQuery(Name = "filter[start]")] string _FilterStart,
            [FromQuery(Name = "filter[end]")] string _FilterEnd
            )
        {
            var _Filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterSalesCode))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "SalesCode",
                    Value = _FilterSalesCode
                });
            }

            if (!string.IsNullOrEmpty(_FilterPosition))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "JobPositionId",
                    Value = _FilterPosition
                });
            }

            if (!string.IsNullOrEmpty(_FilterDealer))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerId",
                    Value = _FilterDealer
                });
            }

            if (!string.IsNullOrEmpty(_FilterCity))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CityId",
                    Value = _FilterCity
                });
            }

            if (!string.IsNullOrEmpty(_FilterBranch))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerBranchId",
                    Value = _FilterBranch
                });
            }

            if (!string.IsNullOrEmpty(_FilterStart))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "StartDate",
                    Value = _FilterStart
                });
            }

            if (!string.IsNullOrEmpty(_FilterEnd))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "EndDate",
                    Value = _FilterEnd
                });
            }

            
            // default start and end date
            var startDate = DateTime.Now.Date.AddDays(-7);
            var endDate = DateTime.Now.AddTicks(-1);

            if (!string.IsNullOrEmpty(_FilterStart) && !string.IsNullOrEmpty(_FilterEnd))
            {
                // default start and end date
                startDate = DateTime.Parse(_FilterStart).Date;
                endDate = DateTime.Parse(_FilterEnd).Date.AddTicks(-1).AddDays(1);
            }

            var Query = new GetUsersReportQuery
            {
                Filters = _Filters,
                StartDate = startDate,
                EndDate = endDate
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/report/bulletin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetBulletinReportDto>> GetBulletinsReport(
            [FromQuery(Name = "filter[dealer]")] string _FilterDealer,
            [FromQuery(Name = "filter[city]")] string _FilterCity,
            [FromQuery(Name = "filter[branch]")] string _FilterBranch,
            [FromQuery(Name = "filter[start]")] string _FilterStart,
            [FromQuery(Name = "filter[end]")] string _FilterEnd
            )
        {
            var _Filters = new List<FilterParams>();

            if (!string.IsNullOrEmpty(_FilterDealer))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerId",
                    Value = _FilterDealer
                });
            }

            if (!string.IsNullOrEmpty(_FilterCity))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CityId",
                    Value = _FilterCity
                });
            }

            if (!string.IsNullOrEmpty(_FilterBranch))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerBranchId",
                    Value = _FilterBranch
                });
            }

            if (!string.IsNullOrEmpty(_FilterStart))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "StartDate",
                    Value = _FilterStart
                });
            }

            if (!string.IsNullOrEmpty(_FilterEnd))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "EndDate",
                    Value = _FilterEnd
                });
            }

            
            // default start and end date
            var startDate = DateTime.Now.Date.AddDays(-7);
            var endDate = DateTime.Now.AddTicks(-1);

            if (!string.IsNullOrEmpty(_FilterStart) && !string.IsNullOrEmpty(_FilterEnd))
            {
                // default start and end date
                startDate = DateTime.Parse(_FilterStart).Date;
                endDate = DateTime.Parse(_FilterEnd).Date.AddTicks(-1).AddDays(1);
            }

            var Query = new GetBulletinReportQuery
            {
                Filters = _Filters,
                StartDate = startDate,
                EndDate = endDate
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/report/info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetInfoReportDto>> GetInfoReport(
            [FromQuery(Name = "filter[dealer]")] string _FilterDealer,
            [FromQuery(Name = "filter[city]")] string _FilterCity,
            [FromQuery(Name = "filter[branch]")] string _FilterBranch,
            [FromQuery(Name = "filter[start]")] string _FilterStart,
            [FromQuery(Name = "filter[end]")] string _FilterEnd
            )
        {
            var _Filters = new List<FilterParams>();

            if (!string.IsNullOrEmpty(_FilterDealer))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerId",
                    Value = _FilterDealer
                });
            }

            if (!string.IsNullOrEmpty(_FilterCity))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CityId",
                    Value = _FilterCity
                });
            }

            if (!string.IsNullOrEmpty(_FilterBranch))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerBranchId",
                    Value = _FilterBranch
                });
            }

            if (!string.IsNullOrEmpty(_FilterStart))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "StartDate",
                    Value = _FilterStart
                });
            }

            if (!string.IsNullOrEmpty(_FilterEnd))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "EndDate",
                    Value = _FilterEnd
                });
            }

            
            // default start and end date
            var startDate = DateTime.Now.Date.AddDays(-7);
            var endDate = DateTime.Now.AddTicks(-1);

            if (!string.IsNullOrEmpty(_FilterStart) && !string.IsNullOrEmpty(_FilterEnd))
            {
                // default start and end date
                startDate = DateTime.Parse(_FilterStart).Date;
                endDate = DateTime.Parse(_FilterEnd).Date.AddTicks(-1).AddDays(1);
            }

            var Query = new GetInfoReportQuery
            {
                Filters = _Filters,
                StartDate = startDate,
                EndDate = endDate
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/report/guide")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetQuickGuideReportDto>> GetQuickGuideReport(
            [FromQuery(Name = "filter[dealer]")] string _FilterDealer,
            [FromQuery(Name = "filter[city]")] string _FilterCity,
            [FromQuery(Name = "filter[branch]")] string _FilterBranch,
            [FromQuery(Name = "filter[category]")] string _FilterCategory,
            [FromQuery(Name = "filter[start]")] string _FilterStart,
            [FromQuery(Name = "filter[end]")] string _FilterEnd,
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType
            )
        {
            var _Filters = new List<FilterParams>();

            if (!string.IsNullOrEmpty(_FilterDealer))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerId",
                    Value = _FilterDealer
                });
            }

            if (!string.IsNullOrEmpty(_FilterCity))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CityId",
                    Value = _FilterCity
                });
            }

            if (!string.IsNullOrEmpty(_FilterBranch))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerBranchId",
                    Value = _FilterBranch
                });
            }

            if (!string.IsNullOrEmpty(_FilterCategory))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CategoryId",
                    Value = _FilterCategory
                });
            }

            if (!string.IsNullOrEmpty(_FilterStart))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "StartDate",
                    Value = _FilterStart
                });
            }

            if (!string.IsNullOrEmpty(_FilterEnd))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "EndDate",
                    Value = _FilterEnd
                });
            }
            
            // default start and end date
            var startDate = DateTime.Now.Date.AddDays(-7);
            var endDate = DateTime.Now.AddTicks(-1);

            if (!string.IsNullOrEmpty(_FilterStart) && !string.IsNullOrEmpty(_FilterEnd))
            {
                // default start and end date
                startDate = DateTime.Parse(_FilterStart).Date;
                endDate = DateTime.Parse(_FilterEnd).Date.AddTicks(-1).AddDays(1);
            }

            var Query = new GetQuickGuideReportQuery
            {
                Filters = _Filters,
                StartDate = startDate,
                EndDate = endDate,
                PagingPage = _PagingPage,
                PagingLimit = _PagingLimit,
                SortColumn = _SortColumn,
                SortType = _SortType,
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/report/training")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetTrainingMaterialReportDto>> GetTrainingMaterialReport(
            [FromQuery(Name = "filter[dealer]")] string _FilterDealer,
            [FromQuery(Name = "filter[city]")] string _FilterCity,
            [FromQuery(Name = "filter[branch]")] string _FilterBranch,
            [FromQuery(Name = "filter[category]")] string _FilterCategory,
            [FromQuery(Name = "filter[start]")] string _FilterStart,
            [FromQuery(Name = "filter[end]")] string _FilterEnd,
            [FromQuery(Name = "paging[page]")] int _PagingPage,
            [FromQuery(Name = "paging[limit]")] int _PagingLimit,
            [FromQuery(Name = "sort[column]")] string _SortColumn,
            [FromQuery(Name = "sort[type]")] string _SortType
            )
        {
            var _Filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterDealer))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerId",
                    Value = _FilterDealer
                });
            }

            if (!string.IsNullOrEmpty(_FilterCity))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CityId",
                    Value = _FilterCity
                });
            }

            if (!string.IsNullOrEmpty(_FilterBranch))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerBranchId",
                    Value = _FilterBranch
                });
            }

            if (!string.IsNullOrEmpty(_FilterCategory))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CategoryId",
                    Value = _FilterCategory
                });
            }

            if (!string.IsNullOrEmpty(_FilterStart))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "StartDate",
                    Value = _FilterStart
                });
            }

            if (!string.IsNullOrEmpty(_FilterEnd))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "EndDate",
                    Value = _FilterEnd
                });
            }
            
            // default start and end date
            var startDate = DateTime.Now.Date.AddDays(-7);
            var endDate = DateTime.Now.AddTicks(-1);

            if (!string.IsNullOrEmpty(_FilterStart) && !string.IsNullOrEmpty(_FilterEnd))
            {
                // default start and end date
                startDate = DateTime.Parse(_FilterStart).Date;
                endDate = DateTime.Parse(_FilterEnd).Date.AddTicks(-1).AddDays(1);
            }

            var Query = new GetTrainingMaterialReportQuery
            {
                Filters = _Filters,
                StartDate = startDate,
                EndDate = endDate,
                PagingPage = _PagingPage,
                PagingLimit = _PagingLimit,
                SortColumn = _SortColumn,
                SortType = _SortType,
            };

            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/report/export-bulletin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ExportBulletinDto>> ExportBulletin(
            [FromQuery(Name = "filter[city]")] string _FilterCity,
            [FromQuery(Name = "filter[dealer]")] string _FilterDealer,
            [FromQuery(Name = "filter[branch]")] string _FilterBranch,
            [FromQuery(Name = "filter[start]")] string _FilterStart,
            [FromQuery(Name = "filter[end]")] string _FilterEnd
        )
        {   
            var _Filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterDealer))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerId",
                    Value = _FilterDealer
                });
            }

            if (!string.IsNullOrEmpty(_FilterCity))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CityId",
                    Value = _FilterCity
                });
            }

            if (!string.IsNullOrEmpty(_FilterBranch))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerBranchId",
                    Value = _FilterBranch
                });
            }

            var startDate = DateTime.Now.Date.AddYears(-100);
            var endDate = DateTime.Now.AddTicks(-1);
            if (!string.IsNullOrEmpty(_FilterStart) && !string.IsNullOrEmpty(_FilterEnd))
            {
                // default start and end date
                startDate = DateTime.Parse(_FilterStart).Date;
                endDate = DateTime.Parse(_FilterEnd).Date.AddTicks(-1).AddDays(1);
            }

            var Query = new ExportBulletinQuery
            {
                Filters = _Filters,
                StartDate = startDate,
                EndDate = endDate,
            };
            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/report/export-info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ExportInfoDto>> ExportInfo(
            [FromQuery(Name = "filter[city]")] string _FilterCity,
            [FromQuery(Name = "filter[dealer]")] string _FilterDealer,
            [FromQuery(Name = "filter[branch]")] string _FilterBranch,
            [FromQuery(Name = "filter[start]")] string _FilterStart,
            [FromQuery(Name = "filter[end]")] string _FilterEnd
        )
        {
            var _Filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterDealer))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerId",
                    Value = _FilterDealer
                });
            }

            if (!string.IsNullOrEmpty(_FilterCity))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CityId",
                    Value = _FilterCity
                });
            }

            if (!string.IsNullOrEmpty(_FilterBranch))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerBranchId",
                    Value = _FilterBranch
                });
            }

            var startDate = DateTime.Now.Date.AddYears(-100);
            var endDate = DateTime.Now.AddTicks(-1);
            if (!string.IsNullOrEmpty(_FilterStart) && !string.IsNullOrEmpty(_FilterEnd))
            {
                // default start and end date
                startDate = DateTime.Parse(_FilterStart).Date;
                endDate = DateTime.Parse(_FilterEnd).Date.AddTicks(-1).AddDays(1);
            }

            var Query = new ExportInfoQuery
            {
                StartDate = startDate,
                EndDate = endDate,
                Filters = _Filters
            };
            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/report/export-guide")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ExportQuickGuideDto>> ExportQuickGuide(
            [FromQuery(Name = "filter[city]")] string _FilterCity,
            [FromQuery(Name = "filter[dealer]")] string _FilterDealer,
            [FromQuery(Name = "filter[branch]")] string _FilterBranch,
            [FromQuery(Name = "filter[category]")] string _FilterCategory,
            [FromQuery(Name = "filter[start]")] string _FilterStart,
            [FromQuery(Name = "filter[end]")] string _FilterEnd
        )
        {
            var _Filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterDealer))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerId",
                    Value = _FilterDealer
                });
            }

            if (!string.IsNullOrEmpty(_FilterCity))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CityId",
                    Value = _FilterCity
                });
            }

            if (!string.IsNullOrEmpty(_FilterBranch))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerBranchId",
                    Value = _FilterBranch
                });
            }

            if (!string.IsNullOrEmpty(_FilterCategory))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "MasterCarId",
                    Value = _FilterCategory
                });
            }

            var startDate = DateTime.Now.Date.AddYears(-100);
            var endDate = DateTime.Now.AddTicks(-1);
            if (!string.IsNullOrEmpty(_FilterStart) && !string.IsNullOrEmpty(_FilterEnd))
            {
                // default start and end date
                startDate = DateTime.Parse(_FilterStart).Date;
                endDate = DateTime.Parse(_FilterEnd).Date.AddTicks(-1).AddDays(1);
            }

            var Query = new ExportQuickGuideQuery
            {
                StartDate = startDate,
                EndDate = endDate,
                Filters = _Filters
            };
            return Ok(await Mediator.Send(Query));
        }

        [HttpGet]
        [Route("/admin/report/export-training")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ExportTrainingMaterialDto>> ExportTrainingMaterial(
            [FromQuery(Name = "filter[city]")] string _FilterCity,
            [FromQuery(Name = "filter[dealer]")] string _FilterDealer,
            [FromQuery(Name = "filter[branch]")] string _FilterBranch,
            [FromQuery(Name = "filter[category]")] string _FilterCategory,
            [FromQuery(Name = "filter[start]")] string _FilterStart,
            [FromQuery(Name = "filter[end]")] string _FilterEnd
        )
        {
            var _Filters = new List<FilterParams>();
            if (!string.IsNullOrEmpty(_FilterDealer))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerId",
                    Value = _FilterDealer
                });
            }

            if (!string.IsNullOrEmpty(_FilterCity))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "CityId",
                    Value = _FilterCity
                });
            }

            if (!string.IsNullOrEmpty(_FilterBranch))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "DealerBranchId",
                    Value = _FilterBranch
                });
            }

            if (!string.IsNullOrEmpty(_FilterCategory))
            {
                _Filters.Add(new FilterParams
                {
                    Column = "MasterCarId",
                    Value = _FilterCategory
                });
            }

            var startDate = DateTime.Now.Date.AddYears(-100);
            var endDate = DateTime.Now.AddTicks(-1);
            if (!string.IsNullOrEmpty(_FilterStart) && !string.IsNullOrEmpty(_FilterEnd))
            {
                // default start and end date
                startDate = DateTime.Parse(_FilterStart).Date;
                endDate = DateTime.Parse(_FilterEnd).Date.AddTicks(-1).AddDays(1);
            }

            var Query = new ExportTrainingMaterialQuery
            {
                StartDate = startDate,
                EndDate = endDate,
                Filters = _Filters
            };
            return Ok(await Mediator.Send(Query));
        }
    }
}
