using anskus.Domain.Account;
using anskus.Domain.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Commands.Delete
{
    internal sealed class DeleteCuestionarioCommandHandler(ICuestionarioRepository _cuestionarioRepository,
        IAccountRepository _accountRepository) : IRequestHandler<DeleteCuestionarioCommand>
    {
        public async Task Handle(DeleteCuestionarioCommand request, CancellationToken cancellationToken)
        {
            Guid user =await _accountRepository.GetUserAsync(request.email);
            if (user!=Guid.Empty)
            {
                await _cuestionarioRepository.RemoveCuestionario(request.idCuestionario, user);
            }
        }
    }
}
