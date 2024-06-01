using anskus.Domain.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Commands.Create
{
    internal sealed class CreateCuestionarioCommandHandler : IRequestHandler<CreateCuestionarioCommand>
    {
        private readonly ICuestionarioRepository _cuestionarioRepository;

        public CreateCuestionarioCommandHandler(ICuestionarioRepository cuestionarioRepository)
        {
            _cuestionarioRepository = cuestionarioRepository;
        }

        public async Task Handle(CreateCuestionarioCommand request, CancellationToken cancellationToken)
        {
            await _cuestionarioRepository.Add(request.Cuestionario);
        }
    }

}
