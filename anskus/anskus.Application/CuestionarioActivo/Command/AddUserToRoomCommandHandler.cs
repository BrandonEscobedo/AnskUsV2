using anskus.Domain.Cuestionarios;
using MediatR;

namespace anskus.Application.CuestionarioActivo.Command
{
    internal sealed class AddUserToRoomCommandHandler(ICuestionarioActivoRepository _cuestionarioActivoRepository)
        : IRequestHandler<AddUserToRoomCommand>
    {
        public async Task Handle(AddUserToRoomCommand request, CancellationToken cancellationToken)
        {

             await _cuestionarioActivoRepository.AddParticipanteToRoomAsync(request.Code, request.Name);
           
        }
    }
}
