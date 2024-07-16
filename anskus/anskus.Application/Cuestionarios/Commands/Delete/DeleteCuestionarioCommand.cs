using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Commands.Delete
{
    public record DeleteCuestionarioCommand(Guid idCuestionario, string email):IRequest;
}
