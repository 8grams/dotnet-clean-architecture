using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.CreateFAQ
{
    public class CreateFAQCommandHandler : IRequestHandler<CreateFAQCommand, CreateFAQDto>
    {
        private readonly ISFDDBContext _context;

        public CreateFAQCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<CreateFAQDto> Handle(CreateFAQCommand request, CancellationToken cancellationToken)
        {
            _context.Faqs.Add(new Domain.Entities.Faq
            {
                Question = request.Data.Question,
                Answer = request.Data.Answer,
                IsActive = request.Data.IsActive,
                CreateBy = request.AdminName
            });
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateFAQDto
            {
                Success = true,
                Message = "FAQ has been successfully created"
            };
        }
    }
}