using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers
{
    public class StateContainerOnPreg: IStateContainerOnPreg
    {
        public Pregunta Pregunta { get; private set; } = new Pregunta();
        public string Titulo { get; set; } = "";
        public event Action? OnIniciarCuestionario;
        public event Action? OnSiguientePregunta;

        public void SetPregunta(Pregunta pregunta)
        {
            Pregunta = pregunta;
            OnSiguientePregunta?.Invoke();
        }

        public void SetTituloPregunta(string Titulo, Pregunta pregunta)
        {
            Pregunta = pregunta;
            this.Titulo = Titulo;
            OnIniciarCuestionario?.Invoke();
        }
    }
}
