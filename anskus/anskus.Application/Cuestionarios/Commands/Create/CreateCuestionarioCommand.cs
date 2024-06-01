﻿using anskus.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Commands.Create
{
    public record CreateCuestionarioCommand (Cuestionario Cuestionario):IRequest;
}
