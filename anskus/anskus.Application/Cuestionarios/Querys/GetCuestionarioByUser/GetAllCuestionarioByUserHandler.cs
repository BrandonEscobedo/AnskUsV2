using anskus.Domain.Cuestionarios;
using anskus.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Querys.GetCuestionarioByUser
{
    internal sealed class GetAllCuestionarioByUserHandler(ICuestionarioRepository cuestionarioRepository) :
        IRequestHandler<GetAllCuestionarioByUser, List<Cuestionario>>
    {
        public async Task<List<Cuestionario>> Handle(GetAllCuestionarioByUser request, CancellationToken cancellationToken)
        {
            return await cuestionarioRepository.GetbyUser(request.Email);
        }
    }
}
