using anskus.Application.DTOs.Response.Cuestionarios;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.ActivarCuestionario
{
    public record ActivarCuestionarioCommand(Guid Idcuestionario, string email) : IRequest<CuestionarioActivoResponse>;
}
