using anskus.Application.DTOs;
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
        public DatosCuestionario DatosCuestionario { get; private set; } = new();
        public int PreguntaActual { get; private set; } = 0;

        public void TiempoTermino()
        {
            OnTiempoTermino?.Invoke();
        }
        public void SetPregunta(Pregunta pregunta)
        {
            Pregunta = pregunta;
            PreguntaActual += 1;
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
        public void SetTituloPregunta(string Titulo, Pregunta pregunta,DatosCuestionario datosCuestionario)
        {
            Pregunta = pregunta;
            this.Titulo = Titulo;
            PreguntaActual += 1;
            this.DatosCuestionario = datosCuestionario;
            OnIniciarCuestionario?.Invoke();
            
        }
    }
}
