using anskus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Domain.Cuestionarios
{
    public class GuardadoState : ICuestionarioState
    {
        public void Activar(Cuestionario cuestionario)
        {
            throw new NotImplementedException();
        }
        public void Borrador(Cuestionario cuestionario)
        {
            Console.WriteLine("Editando");
        }         
        public void Guardar(Cuestionario cuestionario)
        {
            if (!cuestionario.Completo())
            {
                cuestionario.Estado = EstadoCuestionario.Borrador;
                cuestionario.SetEstado(new BorradorState());
            }
        }
    }
}
