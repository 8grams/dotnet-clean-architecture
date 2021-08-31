using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Guide.Material.Command.DeleteGuideMaterial
{
    public class DeleteGuideMaterialCommandHandler : IRequestHandler<DeleteGuideMaterialCommand, DeleteGuideMaterialDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteGuideMaterialCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteGuideMaterialDto> Handle(DeleteGuideMaterialCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var material = await _context.GuideMaterials.FindAsync(id);
                if (material != null) _context.GuideMaterials.Remove(material);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeleteGuideMaterialDto
            {
                Success = true,
                Message = "Guide Material has been successfully deleted"
            };
        }
    }


}