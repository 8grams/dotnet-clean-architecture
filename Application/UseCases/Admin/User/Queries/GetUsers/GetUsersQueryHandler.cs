using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.User.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.User.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;


        public GetUsersQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetUsersDto> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            if (request.Type.Equals("dnet")) {
                return await this._getDNETUser(request);
            }
            return await this._getSFDUser(request);
        }

        private async Task<GetUsersDto> _getDNETUser(GetUsersQuery request) 
        {
            // search for is active filter
            var hasActiveFilter = false;
            var isActive = false;
            foreach (var filter in request.Filters)
            {
                if (filter.Column == "IsActive")
                {
                    hasActiveFilter = true;
                    isActive = filter.Value.Equals("true") ? true : false;
                }
            }
            var userIdsQuery = _context.Users.AsQueryable();
            if (hasActiveFilter)
            {
                userIdsQuery = userIdsQuery.Where(e => e.IsActive == isActive);
            }
            var userIds = userIdsQuery.Select(e => e.UserName).ToList();

            var now = DateTime.Now;
            var data = _context.Salesmen
                .Where(e => userIds.Contains(e.SalesmanCode))
                .AsQueryable();

            foreach (var filter in request.Filters)
            {
                if (filter.Column == "DealerCity")
                {
                    var city = _context.Cities.Find(Int16.Parse(filter.Value));
                    data = data.Where(u => u.DealerCity.Equals(city.CityName));
                }

                if (filter.Column == "DealerBranch")
                {
                    var branch = _context.DealerBranches.Find(int.Parse(filter.Value));
                    data = data.Where(u => u.DealerBranchName.Equals(branch.Name));
                }

                if (filter.Column == "DealerName")
                {
                    var dealer = _context.Dealers.Find(int.Parse(filter.Value));
                    data = data.Where(u => u.DealerName.Equals(dealer.DealerName));
                }

                if (filter.Column == "JobPosition")
                {
                    var job = _context.JobPositions.Find(int.Parse(filter.Value));
                    data = data.Where(u => u.JobDescription.Equals(job.Description));
                }
            }

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("SalesmanCode.Contains(@0) || SalesmanName.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.Salesman, UserData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetUsersDto()
            {
                Success = true,
                Message = "Users are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }

        private async Task<GetUsersDto> _getSFDUser(GetUsersQuery request) 
        {
            // search for is active filter
            var hasActiveFilter = false;
            var isActive = false;
            foreach (var filter in request.Filters)
            {
                if (filter.Column == "IsActive")
                {
                    hasActiveFilter = true;
                    isActive = filter.Value.Equals("true") ? true : false;
                }
            }
            var userIdsQuery = _context.Users.AsQueryable();
            if (hasActiveFilter)
            {
                userIdsQuery = userIdsQuery.Where(e => e.IsActive == isActive);
            }
            var userIds = userIdsQuery.Select(e => e.UserName).ToList();
            
            var now = DateTime.Now;
            var data = _context.SalesmenMeta
                .Include(e => e.User)
                .Where(e => userIds.Contains(e.SalesmanCode));

            foreach (var filter in request.Filters)
            {
                if (filter.Column == "DealerCity")
                {
                    var dealerBranches = _context.DealerBranches
                        .Where(e => e.CityId == Int16.Parse(filter.Value))
                        .Select(e => e.Id)
                        .ToList();
                    data = data.Where(u => dealerBranches.Contains(u.DealerBranchId));
                }

                if (filter.Column == "DealerBranch")
                {
                    data = data.Where(u => u.DealerBranchId == Int16.Parse(filter.Value));
                }

                if (filter.Column == "DealerName")
                {
                    var dealerBranches = _context.DealerBranches
                        .Where(e => e.DealerId == Int16.Parse(filter.Value))
                        .Select(e => e.Id)
                        .ToList();
                    data = data.Where(u => dealerBranches.Contains(u.DealerBranchId));
                }

                if (filter.Column == "JobPosition")
                {
                    data = data.Where(u => u.JobPositionId == int.Parse(filter.Value));
                }
            }

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("SalesmanCode.Contains(@0) || SalesmanName.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"SalesmanCode {request.SortType}")
                .GetPagedAsync<Domain.Entities.SalesmanMeta, UserData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetUsersDto()
            {
                Success = true,
                Message = "Users are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }
    }
}
