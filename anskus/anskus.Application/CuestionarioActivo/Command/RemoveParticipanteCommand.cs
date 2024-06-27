﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.CuestionarioActivo.Command
{
    public record RemoveParticipanteCommand(Guid IdParticipante, int Codigo):IRequest;
}
