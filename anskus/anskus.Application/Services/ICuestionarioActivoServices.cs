using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.Services
{
    public interface ICuestionarioActivoServices
    {
       
        Task<CuestionarioActivoResponse> ActivarCuestionario(Guid? IdCuestionario);
    }
}
