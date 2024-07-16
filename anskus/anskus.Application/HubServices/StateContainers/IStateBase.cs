using anskus.Application.DTOs;
using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers
{
    public interface IStateBase
    {
        public Pregunta Pregunta { get; }
        public string Titulo { get; }
        public event Action? OnIniciarCuestionario;
        public event Action? OnSiguientePregunta;
        public event Action? OnNavegarARanking;
        public void NavegarARanking();
        public void SetTituloPregunta(string Titulo, Pregunta pregunta,DatosCuestionario datosCuestionario);
        public void SetPregunta(Pregunta pregunta);
    }
}
