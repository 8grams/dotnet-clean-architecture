using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using AutoMapper;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.UseCases.Admin.Notification.Models;
using SFIDWebAPI.Application.Extensions;

namespace SFIDWebAPI.Application.UseCases.Admin.Notification.Queries.GetNotifications
{
    public class GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, GetNotificationsDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public GetNotificationsQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetNotificationsDto> Handle(GetNotificationsQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var data = _context.Notifications.AsQueryable();

            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0) || Content.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.Notification, NotificationData>(request.PagingPage, request.PagingLimit, _mapper);

            return new GetNotificationsDto()
            {
                Success = true,
                Message = "Notifications are succefully retrieved",
                Data = results.Data,
                Pagination = results.Pagination
            };
        }
    }
}
