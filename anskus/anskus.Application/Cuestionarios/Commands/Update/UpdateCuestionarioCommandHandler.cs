using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using MediatR;

namespace anskus.Application.Cuestionarios.Commands.Update
{
    internal sealed class UpdateCuestionarioCommandHandler(ICuestionarioRepository _cuestionarioRepository) : IRequestHandler<UpdateCuestionarioCommand,Cuestionario>
    {
      
        public async Task<Cuestionario> Handle(UpdateCuestionarioCommand request, CancellationToken cancellationToken)
        {
          var response=  await _cuestionarioRepository.Update(request.Cuestionario);
            return response;
        }
    }
}
