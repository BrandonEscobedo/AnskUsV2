using anskus.Domain.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.Command
{
    internal sealed class RemoveCuestionarioActivoCommandHandler(ICuestionarioActivoRepository _cuestionarioActivoRepository) : IRequestHandler<RemoveCuestionarioActivoCommand>
    {
        public async Task Handle(RemoveCuestionarioActivoCommand request, CancellationToken cancellationToken)
        {
            await _cuestionarioActivoRepository.RemoveCuestionarioActivo(request.IdCuestionario);
        }
    }
}
