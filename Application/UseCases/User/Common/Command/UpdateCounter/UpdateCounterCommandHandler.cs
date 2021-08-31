using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SFIDWebAPI.Application.Interfaces;
using AutoMapper;

namespace SFIDWebAPI.Application.UseCases.User.Common.Command.UpdateCounter
{
    public class UpdateCounterCommandHandler : IRequestHandler<UpdateCounterCommand, UpdateCounterDto>
    {
        private readonly ISFDDBContext _context;
        private readonly IMapper _mapper;

        public UpdateCounterCommandHandler(ISFDDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateCounterDto> Handle(UpdateCounterCommand request, CancellationToken cancellationToken)
        {
            switch (request.Data.FileType)
            {
                case "bulletin":
                    var bulletin = await _context.Bulletins.FindAsync(request.Data.FileId);
                    bulletin.TotalViews = bulletin.TotalViews + 1;
                    _context.Bulletins.Update(bulletin);
                    break;
                case "info":
                    var info = await _context.AdditionalInfos.FindAsync(request.Data.FileId);
                    info.TotalViews = info.TotalViews + 1;
                    _context.AdditionalInfos.Update(info);
                    break;
                case "guide":
                    var guide = await _context.GuideMaterials.FindAsync(request.Data.FileId);
                    guide.TotalViews = guide.TotalViews + 1;
                    _context.GuideMaterials.Update(guide);
                    break;
                case "training":
                    var training = await _context.TrainingMaterials.FindAsync(request.Data.FileId);
                    training.TotalViews = training.TotalViews + 1;
                    _context.TrainingMaterials.Update(training);
                    break;
                default:
                    throw new InvalidOperationException("Cannot process");
            }

            _context.MaterialCounters.Add(new Domain.Entities.MaterialCounter
            {
                ContentId = request.Data.FileId,
                ContentType = request.Data.FileType,
                UserId = request.UserId
            });

            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateCounterDto()
            {
                Success = true,
                Message = "Total views updated",
                Origin = "common.success.default"
            };
        }
    }
}
