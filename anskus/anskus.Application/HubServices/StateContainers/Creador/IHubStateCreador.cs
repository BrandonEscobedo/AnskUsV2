using anskus.Application.DTOs;
using anskus.Application.DTOs.Response.Cuestionarios;
using anskus.Domain.Models;
namespace anskus.Application.HubServices.StateContainers.Creador
{
    public interface IHubStateCreador
    {
        public CuestionarioResponse Cuestionario { get; set; }
        public int Codigo { get; set; }
        public event Action? CuestionarioFinalizo;
        public DatosCuestionario DatosCuestionario { get; set; }
        public void SetCuestionario(CuestionarioResponse Cuestionario, int Codigo);
        public Pregunta MandarSiguientePregunta();
    }
}
