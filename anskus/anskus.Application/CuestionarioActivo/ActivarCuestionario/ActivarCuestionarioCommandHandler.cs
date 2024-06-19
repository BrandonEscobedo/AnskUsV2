using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Domain.Cuestionarios;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.ActivarCuestionario
{
    internal sealed class ActivarCuestionarioCommandHandler(ICuestionarioActivoService cuestionarioActivoRepository
        , IMapper mapper)
        : IRequestHandler<ActivarCuestionarioCommand, CuestionarioActivoResponse>
    {
        public async Task<CuestionarioActivoResponse> Handle(ActivarCuestionarioCommand request, CancellationToken cancellationToken)
        {
            var response =await cuestionarioActivoRepository.ActivarCuestionario(request.Idcuestionario, request.email);
            if (response != null)
            {
                var result = mapper.Map<CuestionarioActivoResponse>(response);
                return result;
            }
            return null!;

        }
    }
}
