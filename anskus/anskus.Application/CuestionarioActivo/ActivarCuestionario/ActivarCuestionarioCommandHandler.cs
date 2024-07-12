using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Domain.Account;
using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using AutoMapper;
using MediatR;
namespace anskus.Application.CuestionarioActivo.ActivarCuestionario
{
    internal sealed class ActivarCuestionarioCommandHandler(ICuestionarioActivoService cuestionarioActivoService, IAccountRepository _accountRepository
        , IMapper mapper)
        : IRequestHandler<ActivarCuestionarioCommand, CuestionarioActivoResponse>
    {
        public async Task<CuestionarioActivoResponse> Handle(ActivarCuestionarioCommand request, CancellationToken cancellationToken)
        {
            var userId =await _accountRepository.GetUserAsync(request.email);
            if (userId==Guid.Empty)
            {
                throw new Exception("Usuario no encontrado");
            }
            var response = await cuestionarioActivoService.ActivarCuestionario(request.Idcuestionario, userId);
            if (response != null)
            {
                foreach( Pregunta pregunta in response.Cuestionario.Pregunta)
                {
                    pregunta.Respuesta = pregunta.Respuesta
                        .Where(x => !string.IsNullOrEmpty(x.respuesta)).ToList();
                }
                var result = mapper.Map<CuestionarioActivoResponse>(response);
                return result;
            }
            return null!;
        }
    }
}
