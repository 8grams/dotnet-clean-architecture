using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Misc;

namespace SFIDWebAPI.Application.UseCases.User.UserProfile.Queries.CheckNotification
{
    public class CheckNotificationQueryHandler : IRequestHandler<CheckNotificationQuery, CheckNotificationDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMemoryData _memoryData;

        public CheckNotificationQueryHandler(ISFDDBContext context, IMapper mapper, IMemoryData memoryData)
        {
            _context = context;
            _memoryData = memoryData;
        }

        public async Task<CheckNotificationDto> Handle(CheckNotificationQuery request, CancellationToken cancellationToken)
        {
            var updates = await _context.MaterialStatuses
                .Where(e => e.UserId == request.UserId)
                .FirstOrDefaultAsync();

            if (updates == null)
            {
                updates = new Domain.Entities.MaterialStatus
                {
                    UserId = request.UserId,
                    NewBulletin = 0,
                    NewGuide = 0,
                    NewInfo = 0,
                    NewTraining = 0
                };

                _context.MaterialStatuses.Add(updates);
                await _context.SaveChangesAsync(cancellationToken);
            }

            // check notifications
            var user = await _context.Users.FindAsync(request.UserId);
            var topics = Utils.GetAllTopics(user.Salesman.LevelDescription, user.Salesman.DealerGroup, user.Salesman.DealerCity);
            var notifStatus = await _context.NotificationStatuses
                .Where(e => e.UserId == user.Id)
                .Where(e => e.HasRead == true)
                .Select( e => e.NotificationId)
                .ToListAsync();
            var isExists = await _context.Notifications
                .Where(e => e.OwnerId.Equals(request.UserId.ToString()) || topics.Contains(e.OwnerId) )
                .Where(e => !notifStatus.Contains(e.Id))
                .Where(e => e.CreateDate >= user.CreateDate)
                .CountAsync();

            // set material status
            if (_memoryData.NewBulletin == 0) {
                _memoryData.NewBulletin = _context.Bulletins.OrderByDescending(e => e.CreateDate).First().Id;
            }

            if (_memoryData.NewInfo == 0) {
                _memoryData.NewInfo = _context.AdditionalInfos.OrderByDescending(e => e.CreateDate).First().Id;
            }

            if (_memoryData.NewTraining == 0) {
                _memoryData.NewTraining = _context.TrainingMaterials.OrderByDescending(e => e.CreateDate).First().Id;
            }

            if (_memoryData.NewGuide == 0) {
                _memoryData.NewGuide = _context.GuideMaterials.OrderByDescending(e => e.CreateDate).First().Id;
            }

            var responseData = new CheckNotificationData
            {
                HasNewBulletin = _memoryData.NewBulletin > 0 && updates.NewBulletin != _memoryData.NewBulletin,
                HasNewInfo = _memoryData.NewInfo > 0 && updates.NewInfo != _memoryData.NewInfo,
                HasNewGuide = _memoryData.NewGuide > 0 && updates.NewGuide != _memoryData.NewGuide,
                HasNewTraining = _memoryData.NewTraining > 0 && updates.NewTraining != _memoryData.NewTraining,
                HasNewNotification = isExists > 0
            };
            
            return new CheckNotificationDto()
            {
                Success = true,
                Message = "Material update is succefully retrieved",
                Data = responseData
            };
        }
    }
}
