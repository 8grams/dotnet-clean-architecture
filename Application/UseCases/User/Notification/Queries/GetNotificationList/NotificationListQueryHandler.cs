using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Extensions;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.UseCases.User.Notification.Queries.GetNotificationList
{
    public class NotificationListQueryHandler : IRequestHandler<NotificationListQuery, NotificationListDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public NotificationListQueryHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NotificationListDto> Handle(NotificationListQuery request, CancellationToken cancellationToken)
        {
            // get deleted filter
            var deletedIds = await _context.NotificationStatuses
                .Where(e => e.IsDeleted == true)
                .Where(e => e.UserId == request.UserId)
                .Select(e => e.NotificationId)
                .ToListAsync();

            var user = await _context.Users
                .Include(e => e.SalesmanData)
                .Where(e => e.Id == request.UserId)
                .FirstAsync();

            var topics = Utils.GetAllTopics(user.Salesman.JobDescription, user.Salesman.DealerGroup, user.Salesman.DealerCity);
            var data = _context.Notifications
                .Where(e => e.OwnerId.Equals(request.UserId.ToString()) || topics.Contains(e.OwnerId) )
                .Where(e => !deletedIds.Contains(e.Id))
                .Where(e => e.CreateDate >= user.CreateDate);
   
            if (!string.IsNullOrEmpty(request.QuerySearch))
            {
                data = data.Where("Title.Contains(@0) or Content.Contains(@0)", request.QuerySearch);
            }

            var results = await data.OrderBy($"{request.SortColumn} {request.SortType}")
                .GetPagedAsync<Domain.Entities.Notification, NotificationData>(request.PagingPage, request.PagingLimit, _mapper);

            // define has read or not here
            var response = new List<NotificationData>();
            foreach (var item in results.Data)
            {
                var check = await _context.NotificationStatuses
                    .Where(e => e.NotificationId == item.Id)
                    .Where(e => e.UserId == request.UserId)
                    .FirstOrDefaultAsync();
               item.HasRead = check != null && check.HasRead == true;
               response.Add(item);
            }

            return new NotificationListDto()
            {
                Success = true,
                Message = "Notifications succefully retrieved",
                Data = response,
                Pagination = results.Pagination
            };
        }
    }
}
