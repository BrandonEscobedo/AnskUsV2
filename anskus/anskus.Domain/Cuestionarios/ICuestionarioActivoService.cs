using anskus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Cuestionarios
{
    public interface ICuestionarioActivoService
    {
        Task<CuestionarioActivo> ActivarCuestionario(Guid Idcuestionario, string email);
    }
}
