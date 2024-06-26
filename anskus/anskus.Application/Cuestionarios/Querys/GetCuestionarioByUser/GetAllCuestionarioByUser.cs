﻿using anskus.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Cuestionarios.Querys.GetCuestionarioByUser
{
    public record GetAllCuestionarioByUser(string Email):IRequest<List<Cuestionario>>;
}
