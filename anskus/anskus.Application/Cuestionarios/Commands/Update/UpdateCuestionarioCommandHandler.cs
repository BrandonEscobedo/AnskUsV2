using anskus.Domain.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Commands.Update
{
    internal sealed class UpdateCuestionarioCommandHandler(ICuestionarioRepository _cuestionarioRepository) : IRequestHandler<UpdateCuestionarioCommand>
    {
      
        public async Task Handle(UpdateCuestionarioCommand request, CancellationToken cancellationToken)
        {
            await _cuestionarioRepository.Update(request.Cuestionario);
        }
    }
}
