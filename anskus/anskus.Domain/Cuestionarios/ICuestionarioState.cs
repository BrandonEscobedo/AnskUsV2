using anskus.Domain.Models;
namespace anskus.Domain.Cuestionarios
{
    public interface ICuestionarioState
    {
        void Borrador(Cuestionario cuestionario);
        void Guardar(Cuestionario cuestionario);
        void Activar(Cuestionario cuestionario);
    }
}
