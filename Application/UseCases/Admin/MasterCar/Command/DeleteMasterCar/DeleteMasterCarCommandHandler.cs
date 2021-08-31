using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFIDWebAPI.Application.Interfaces;

namespace SFIDWebAPI.Application.UseCases.Admin.MasterCar.Command.DeleteMasterCar
{
    public class DeleteMasterCarCommandHandler : IRequestHandler<DeleteMasterCarCommand, DeleteMasterCarDto>
    {
        private readonly ISFDDBContext _context;

        public DeleteMasterCarCommandHandler(ISFDDBContext context)
        {
            _context = context;
        }

        public async Task<DeleteMasterCarDto> Handle(DeleteMasterCarCommand request, CancellationToken cancellationToken)
        {
            foreach (var id in request.Ids)
            {
                var category = await _context.MasterCars.FindAsync(id);
                if (category != null 
                    && category.TrainingMaterials.Count == 0
                    && category.GuideMaterials.Count == 0
                    ) 
                {
                    _context.MasterCars.Remove(category);
                }
                else
                {
                    return new DeleteMasterCarDto
                    {
                        Success = true,
                        Message = "Master Car cannot be deleted"
                    };
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return new DeleteMasterCarDto
            {
                Success = true,
                Message = "Training Category has been successfully deleted"
            };
        }
    }


}