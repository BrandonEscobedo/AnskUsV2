using anskus.Domain.Account;
using anskus.Domain.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.Command
{
    internal sealed class RemoveCuesActFromUserCommandHandler(IAccountRepository _accountRepository,
        ICuestionarioActivoRepository _cuestionarioActivoRepository,
        ICuestionarioRepository _cuestionarioRepository
        ) : IRequestHandler<RemoveCuesActFromUserCommand>
    {
        public async Task Handle(RemoveCuesActFromUserCommand request, CancellationToken cancellationToken)
        {
            var userId = await _accountRepository.GetUserAsync(request.email);
            var cuestionario = await _cuestionarioRepository.GetbyId(request.Idcuestionario, userId);
            cuestionario.Estado = Domain.Models.EstadoCuestionario.Guardado;
            await _cuestionarioRepository.Update(cuestionario);
            await _cuestionarioActivoRepository.RemoveCuestionarioActivo(request.Idcuestionario);    
        }
    }
}
