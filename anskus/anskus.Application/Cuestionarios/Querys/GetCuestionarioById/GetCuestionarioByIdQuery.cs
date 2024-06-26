﻿using anskus.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Querys.GetCuestionarioById
{
    public record GetCuestionarioByIdQuery(Guid id, string email):IRequest<Cuestionario>;

}
