using anskus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Cuestionarios
{
    public interface ICuestionarioActivoRepository
    {
        Task<bool> AddParticipanteCuestionarioAsync(string code, string name);
        Task<CuestionarioActivo>     ActivarCuestionarioAsync(Guid idcuestionario, string email);  
    }
}
