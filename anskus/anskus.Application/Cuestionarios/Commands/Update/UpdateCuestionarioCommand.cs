using anskus.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Commands.Update
{
    public record UpdateCuestionarioCommand(Cuestionario Cuestionario):IRequest<Cuestionario>;
}
