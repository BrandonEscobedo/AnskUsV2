using anskus.Domain.Cuestionarios;
using MediatR;

namespace anskus.Application.CuestionarioActivo.Command
{
    internal sealed class AddUserToRoomCommandHandler(ICuestionarioActivoRepository _cuestionarioActivoRepository)
        : IRequestHandler<AddUserToRoomCommand,Guid>
    {
        public async Task<Guid> Handle(AddUserToRoomCommand request, CancellationToken cancellationToken)
        {
            return await _cuestionarioActivoRepository.AddParticipanteToRoomAsync(request.Code, request.Name);           
        }
    }
}
