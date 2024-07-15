using anskus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.DTOs.Response.Cuestionarios
{
    public class CuestionarioActivoResponse
    {
        public Guid IdcuestionarioActivo { get; set; }
        public int Codigo { get; set; }
        public CuestionarioResponse Cuestionario { get; set; } = new CuestionarioResponse();
        public Guid IdUsuario { get; set; }

    }
}
