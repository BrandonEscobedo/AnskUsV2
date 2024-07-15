using anskus.Domain.Account;
using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using MediatR;

namespace anskus.Application.Cuestionarios.Querys.GetCuestionarioById
{
    internal sealed class GetCuestionarioByIdQueryHandler(ICuestionarioRepository _cuestionarioRepository, IAccountRepository _accountRepository)
        : IRequestHandler<GetCuestionarioByIdQuery,Cuestionario>
    {
        public async Task<Cuestionario> Handle(GetCuestionarioByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = await _accountRepository.GetUserAsync(request.email);
            return await _cuestionarioRepository.GetbyId(request.id,userId);
        }
    }
}
