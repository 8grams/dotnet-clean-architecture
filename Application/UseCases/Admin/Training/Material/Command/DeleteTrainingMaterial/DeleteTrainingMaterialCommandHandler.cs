using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.Training.Material.Command.DeleteTrainingMaterial
{
    public class DeleteTrainingMaterialCommandHandler : IRequestHandler<DeleteTrainingMaterialCommand, DeleteTrainingMaterialDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteTrainingMaterialCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteTrainingMaterialDto> Handle(DeleteTrainingMaterialCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var material = await _context.TrainingMaterials.FindAsync(id);
                if (material != null) _context.TrainingMaterials.Remove(material);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeleteTrainingMaterialDto
            {
                Success = true,
                Message = "Training Material has been successfully deleted"
            };
        }
    }


}