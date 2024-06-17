using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Querys.GetCuestionarioById
{
    internal sealed class GetCuestionarioByIdQueryHandler(ICuestionarioRepository _cuestionarioRepository)
        : IRequestHandler<GetCuestionarioByIdQuery,Cuestionario>
    {
        public Task<Cuestionario> Handle(GetCuestionarioByIdQuery request, CancellationToken cancellationToken)
        {
            return _cuestionarioRepository.GetbyId(request.id,request.email);
        }
    }
}
