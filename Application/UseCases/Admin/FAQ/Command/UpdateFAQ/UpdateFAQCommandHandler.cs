using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;
using SFIDWebAPI.Application.Exceptions;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.UpdateFAQ
{
    public class UpdateFAQCommandHandler : IRequestHandler<UpdateFAQCommand, UpdateFAQDto>
    {
        private readonly ISFDDBContext _context;

        public UpdateFAQCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<UpdateFAQDto> Handle(UpdateFAQCommand request, CancellationToken cancellationToken)
        {
            var faq = await _context.Faqs.FindAsync(request.Data.Id);
            if (faq == null) throw new NotFoundException(nameof(Domain.Entities.Faq), request.Data.Id);

            faq.Answer = request.Data.Answer;
            faq.Question = request.Data.Question;
            faq.IsActive = request.Data.IsActive;
            faq.LastUpdateBy = request.AdminName;
            
            _context.Faqs.Update(faq);
            await _context.SaveChangesAsync(cancellationToken);

            return new UpdateFAQDto
            {
                Success = true,
                Message = "FAQ has been successfully updated"
            };
        }
    }
}