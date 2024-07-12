using anskus.Domain.Models;

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
