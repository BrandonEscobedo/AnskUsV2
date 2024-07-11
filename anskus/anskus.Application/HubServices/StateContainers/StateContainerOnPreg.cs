using anskus.Domain.Models;

namespace anskus.Application.HubServices.StateContainers
{
    public class StateContainerOnPreg: IStateContainerOnPreg
    {
        public Pregunta Pregunta { get; private set; } = new Pregunta();
        public string Titulo { get; set; } = "";
        public event Action? OnIniciarCuestionario;
        public event Action? OnSiguientePregunta;
        public event Action? OnNavegarARanking;
        public event Action? OnNavegarAClasificacion;
        public event Action? OnTiempoTermino;
        public void TiempoTermino()
        {
            OnTiempoTermino?.Invoke();
        }
        public void SetPregunta(Pregunta pregunta)
        {
            Pregunta = pregunta;
            OnSiguientePregunta?.Invoke();
        }
        public void NavegarARanking()
        {
            OnNavegarARanking?.Invoke();
        }
        public void NavegarAClasificacion()
        {
            OnNavegarAClasificacion?.Invoke();
        }
        public void SetTituloPregunta(string Titulo, Pregunta pregunta)
        {
            Pregunta = pregunta;
            this.Titulo = Titulo;
            OnIniciarCuestionario?.Invoke();
        }
    }
}
