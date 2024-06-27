using anskus.Domain.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.Command
{
    internal sealed class RemoveParticipanteCommandHandler(ICuestionarioActivoRepository _cuestionarioActivoRepository) : IRequestHandler<RemoveParticipanteCommand>
    {
        public async Task Handle(RemoveParticipanteCommand request, CancellationToken cancellationToken)
        {
            await _cuestionarioActivoRepository.RemoveParticipanteFromRoom(request.IdParticipante, request.Codigo);
        }
    }
}
