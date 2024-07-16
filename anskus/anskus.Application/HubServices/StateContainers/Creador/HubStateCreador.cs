using anskus.Application.DTOs;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Domain.Models;
namespace anskus.Application.HubServices.StateContainers.Creador
{
    public class HubStateCreador : IHubStateCreador
    {
        public CuestionarioResponse Cuestionario { get; set; } = new();
        public int Codigo { get; set; }
        public event Action? CuestionarioFinalizo;
        public DatosCuestionario DatosCuestionario { get; set; } = new();
        public Pregunta MandarSiguientePregunta()
        {
                var pregunta = Cuestionario.Pregunta.First();
                Cuestionario.Pregunta.RemoveAt(0);
                return pregunta;
            
        }
        //Buscar Implementar esto en cache
        public void SetCuestionario(CuestionarioResponse Cuestionario, int Codigo)
        {
            this.Cuestionario = Cuestionario;
            this.Codigo = Codigo;
            DatosCuestionario.CantidadPreguntas = Cuestionario.Pregunta.Count;
            DatosCuestionario.TiempoPorPregunta = Cuestionario.TiempoRespuesta;
        }
    }
}
