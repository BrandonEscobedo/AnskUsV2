using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Commands.Create
{
    internal sealed class CreateCuestionarioCommandHandler : IRequestHandler<CreateCuestionarioCommand,Cuestionario>
    {
        private readonly ICuestionarioRepository _cuestionarioRepository;

        public CreateCuestionarioCommandHandler(ICuestionarioRepository cuestionarioRepository)
        {
            _cuestionarioRepository = cuestionarioRepository;
        }

        public async Task<Cuestionario> Handle(CreateCuestionarioCommand request, CancellationToken cancellationToken)
        {
            var result=  await _cuestionarioRepository.Add(request.Cuestionario,request.email);
            return result;
        }
    }

}
