using anskus.Domain.Cuestionarios;
using MediatR;
namespace anskus.Application.CuestionarioActivo.Command
{
    internal sealed class RemoveCuestionarioActivoCommandHandler(ICuestionarioActivoRepository _cuestionarioActivoRepository, ICuestionarioRepository _cuestionarioRepository) : IRequestHandler<RemoveCuestionarioActivoCommand>
    {
        public async Task Handle(RemoveCuestionarioActivoCommand request, CancellationToken cancellationToken)
        {
            await _cuestionarioActivoRepository.RemoveCuestionarioActivo(request.IdCuestionario);
            var cuestionario = await _cuestionarioRepository.GetbyId(request.IdCuestionario, request.IdUsuario);
            cuestionario.Estado = Domain.Models.EstadoCuestionario.Guardado;
            await _cuestionarioRepository.Update(cuestionario);
        }
    }
}
