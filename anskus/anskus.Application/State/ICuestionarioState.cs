using anskus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Application.State
{
    public interface ICuestionarioState
    {
        void Borrador(Cuestionario cuestionario);
        void Guardar(Cuestionario cuestionario);
        void Activar(Cuestionario cuestionario);
    }
}
