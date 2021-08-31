using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.FAQ.Command.DeleteFAQ
{
    public class DeleteFAQCommandHandler : IRequestHandler<DeleteFAQCommand, DeleteFAQDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteFAQCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteFAQDto> Handle(DeleteFAQCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var faq = await _context.Faqs.FindAsync(id);
                if (faq != null) _context.Faqs.Remove(faq);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeleteFAQDto
            {
                Success = true,
                Message = "FAQ has been successfully deleted"
            };
        }
    }


}